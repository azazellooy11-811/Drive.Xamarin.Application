
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Java.Net;
using Newtonsoft.Json;

namespace DriveXamarin
{
    [Activity(Label = "SignupActivity")]
    public class SignupActivity : Activity
    {
        public User User { get; set; }
        private EditText mTxtFirstName;
        private EditText mTxtSurname;
        private EditText mTxtCity;
        private EditText mTxtUserName;
        private EditText mTxtDrivingSchool;
        private EditText mTxtPassword;
        private int statusCode = 0;
        private Button mBtnSignUp;
        private static readonly HttpClient client = new HttpClient();
        private readonly string url = "http://10.0.2.2:5000/driveapi/user/Users/Register";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_sign_up);

            mTxtFirstName = FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtSurname = FindViewById<EditText>(Resource.Id.txtSurName);
            mTxtCity = FindViewById<EditText>(Resource.Id.txtCity);
            mTxtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            mTxtDrivingSchool = FindViewById<EditText>(Resource.Id.txtDrivingSchool);
            mTxtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnDialogSignUp);
            mBtnSignUp.Click += mBtnSignUp_Click;

        }

        private async void mBtnSignUp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mTxtUserName.Text) && !string.IsNullOrEmpty(mTxtFirstName.Text) && !string.IsNullOrEmpty(mTxtCity.Text) && !string.IsNullOrEmpty(mTxtPassword.Text) && !string.IsNullOrEmpty(mTxtSurname.Text) && !string.IsNullOrEmpty(mTxtDrivingSchool.Text))
            {
                //var User = new User()
                //{
                //    FirstName = mTxtFirstName.Text,
                //    LastName = mTxtSurname.Text,
                //    City = mTxtCity.Text,
                //    Username = mTxtUserName.Text,
                //    DrivingSchool = mTxtDrivingSchool.Text,
                //    Token = mTxtPassword.Text,
                //};
                //if (User != null)
                //{
                    Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    {"firstName", $"{mTxtFirstName.Text}" },
                    {"lastName", $"{mTxtSurname.Text}"},
                    {"city", $"{mTxtCity.Text}" },
                    {"username", $"{mTxtUserName.Text}" },
                    {"drivingSchool", $"{mTxtDrivingSchool.Text}" },
                    {"passwordHash", $"{mTxtPassword.Text}" }

                };
                    var parameters = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = WebRequestMethods.Http.Post;
                    req.ContentType = "application/json";
                    req.AllowAutoRedirect = false;
                    req.Accept = @"*/*";

                    byte[] bytes = Encoding.ASCII.GetBytes(parameters);

                    req.ContentLength = bytes.Length;

                    using (var os = req.GetRequestStream())
                    {
                        os.Write(bytes, 0, bytes.Length);

                        os.Close();
                    }
                    var stream = req.GetResponse().GetResponseStream();
                    if (stream != null)
                        using (stream)
                        using (var sr = new StreamReader(stream))
                        {
                            sr.ReadToEnd().Trim();
                        }
                    var response = (HttpWebResponse)await req.GetResponseAsync();
                statusCode = (int)response.StatusCode;

                if (statusCode == 200)
                {
                    Toast.MakeText(Application.Context, "Вы успешно зарегистрировались", ToastLength.Short).Show();
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    LoginFrgActivity logInDialog = new LoginFrgActivity();
                    logInDialog.Show(transaction, "dialog fragment");
                    
                }
                else
                {
                    Toast.MakeText(Application.Context, "Регистрация не прошла", ToastLength.Short).Show();
                }

            }
            else
            {
                Toast.MakeText(Application.Context, "Заполните все ячейки", ToastLength.Short).Show();
            }
        }

        async Task<string> PostJson(string uri, User postParameters)
        {
            string postData = JsonConvert.SerializeObject(postParameters);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.3 Safari/605.1.15");

            var response = await client.PostAsync(uri, new StringContent(postData.ToString(), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }

    }


