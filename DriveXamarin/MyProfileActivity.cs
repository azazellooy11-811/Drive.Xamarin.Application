
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DriveXamarin.Models;
using Newtonsoft.Json;

namespace DriveXamarin
{
    [Activity(Label = "MyProfileActivity")]
    [Obsolete]
    public class MyProfileActivity : Activity
    {
        public static string _currentUsername;
        public static int _currentPoints;
        public static long _currenUserId;
        private TextView _TxtUsername;
        private TextView _TxtUserPoints;
        public Button _badges;
        public static ListView _listViewLiders;
        public static List<string> items = new List<string>();
        public static List<string> itemsPoints = new List<string>();
        public static List<User> resultUser = new List<User>();
        //private Button _LiderBoard;
        public static HttpClient client = new HttpClient();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_my_profile);
            
            _TxtUsername = FindViewById<TextView>(Resource.Id.username);
            _TxtUserPoints = FindViewById<TextView>(Resource.Id.lblScore);
            var url = "http://10.0.2.2:5000/driveapi/user/Users/Read";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + LoginFrgActivity.accessToken);
            var response = await client.GetStringAsync(url);
            dynamic responseObject = JsonConvert.DeserializeObject(response);
            _currentUsername = responseObject.username;
            _currentPoints = responseObject.points;
            _currenUserId = responseObject.id;
            
            _TxtUsername.Text = $"Ник:{_currentUsername}";
            _TxtUserPoints.Text = $"Счёт:{_currentPoints.ToString()}";

            GetAllUsers();
            _listViewLiders = (ListView)FindViewById<ListView>(Resource.Id.list);
            _badges = FindViewById<Button>(Resource.Id.badge);
            _badges.Click += BadgesActivityGo;

            
        }
        private void BadgesActivityGo(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(BadgesActivity));
            this.StartActivity(intent);

        }
        public static async Task<string> GetAllUsers()
        {
            var resultJson = await client.GetAsync("http://10.0.2.2:5000/driveapi/user/Users/AllUser");

            List<User> resultUser = JsonConvert.DeserializeObject<List<User>>(await resultJson.Content.ReadAsStringAsync());
            for (var i = 0; i < resultUser.Count; i++)
            {
                User UsersUsername = resultUser[i];
                items.Add(UsersUsername.Username);
                itemsPoints.Add(UsersUsername.Points.ToString());
            }
            _listViewLiders.Adapter = new ArrayAdapter(Application.Context, Resource.Layout.activity_liderboard, items);
            _listViewLiders.FindViewById<TextView>(Resource.Id.topUsersLeaderboardusername).Text = items.ToString();
            _listViewLiders.FindViewById<TextView>(Resource.Id.topUsersLeaderboardPoints).Text = itemsPoints.ToString();

            //_listViewLiders.Adapter = new HomeScreenAdapter(And, items);
            return items.ToString();
        }

    }
}
