
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
    [Activity(Label = "LiderBoardActivity")]
    [Obsolete]
    public class LiderBoardActivity : ListActivity
    {
        public static HttpClient client = new HttpClient();
        public TextView _usersWithLiderBoard;
        public static List<User> User = new List<User>();
        public static List<User> _curentUsers = new List<User>();
        public static string[] resultUsers = new String[]
        { "1   azaliia            58",
          "2   azaliia            58",
          "2   azaliia            58",};
        public static string aba;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.activity_liderboard, resultUsers);
         
            //_usersWithLiderBoard = FindViewById<TextView>(Resource.Id.topUsersLeaderboardItem_username);
            
        }
        public static async Task GetUsersWithLiderBoard()
        {
           var resultUserLiderboard = await client.GetStringAsync("http://10.0.2.2:5000/driveapi/user/Users/AllUser");
            dynamic responseObject = JsonConvert.DeserializeObject(resultUserLiderboard);
            var b = responseObject.username;
            foreach (var s in b)
            {

            }
            
        }
        
    }
}
