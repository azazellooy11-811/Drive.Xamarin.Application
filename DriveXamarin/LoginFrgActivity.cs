
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
    [Obsolete]
    public class LoginFrgActivity : DialogFragment
    {
        public User User { get; set; }
        private Button btnLogin { get; set; }
        private EditText mTxtUserName { get; set; }
        private EditText mTxtPassword { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private int statusCode = 0;
        public static string accessToken;
        private readonly string url = "http://10.0.2.2:5000/driveapi/user/Users/Authenticate";//http://10.0.2.2:5000/driveapi/user/Users/Authenticate

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.activity_log_in_dialog, container, false);
            mTxtUserName = view.FindViewById<EditText>(Resource.Id.txtUserName);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            btnLogin = view.FindViewById<Button>(Resource.Id.btnDialogLogIn);
            btnLogin.Click += BtnLogin_Click;
            return view;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mTxtUserName.Text) && !string.IsNullOrEmpty(mTxtPassword.Text))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    {"username", $"{mTxtUserName.Text}" },
                    {"password", $"{mTxtPassword.Text}"}
                };
                var json = JsonConvert.SerializeObject(dict);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(result);
               accessToken = responseObject.token;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Intent intent = new Intent(Application.Context, typeof(MainActivity));  //the activity you want to open
                    this.StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(Application.Context, "Неправильные логин или пароль", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Заполните все ячейки", ToastLength.Short).Show();
            }
        }
        //public static async Task<string> GetToken(string _username, string _password)
        //{
        //    var content = new
        //    {
        //        username = $"{_username}",
        //        password = $"{_password}"
        //    };
        //    var httpClient = new HttpClient();
        //    var serializedContent = JsonConvert.SerializeObject(content);
        //    var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
        //    var result =
        //        await httpClient.PostAsync($"https://localhost:44393/driveapi/user/Users/Authenticate", stringContent);
        //    var resultContent = await result.Content.ReadAsStringAsync();
        //    dynamic responseObject = JsonConvert.DeserializeObject(resultContent);
        //    return responseObject.token;
        //}
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.SwipeToDismiss); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState); 
        }
    }
}
