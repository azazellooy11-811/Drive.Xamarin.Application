
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace DriveXamarin
{
    [Activity(Label = "UpdatePasswordActivity")]
    public class UpdatePasswordActivity : Activity
    {
        public EditText existUsername;
        public EditText newPassword;
        public Button okButton;
        HttpClient client = new HttpClient();
        public static readonly string url = "http://10.0.2.2:5000/driveapi/user/Users/Profile";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_update_password);
            existUsername = FindViewById<EditText>(Resource.Id.txtUserName);
            newPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            okButton = FindViewById<Button>(Resource.Id.updateBtn);
            okButton.Click += BtnLogin_Click;

        }
        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(existUsername.Text) && !string.IsNullOrEmpty(newPassword.Text))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + LoginFrgActivity.accessToken);
                Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    {"passwordHash", $"{newPassword.Text}"}
                };

                var serializedContent = JsonConvert.SerializeObject(dict);
                var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, stringContent);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    Toast.MakeText(Application.Context, "Пароль успешно поменялся", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(Application.Context, "Ошибка", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(Application.Context, "Заполните все ячейки", ToastLength.Short).Show();
            }
        }
    }
}
