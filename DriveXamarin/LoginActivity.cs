
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DriveXamarin.Models;

namespace DriveXamarin
{
    [Activity(Label = "LoginActivity", NoHistory = true, MainLauncher = true)]
    public class LoginActivity : Activity
    {
        public static LoginActivity loginScreen { get; set; }
        private Button mBtnLogIn { get; set; }
        private Button mBtnSignUp { get; set; }
        private ProgressBar mProgressBar { get; set; }
        public LinearLayout _layout { get; set; }
        public static User _user { get; set; }

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnLogIn = FindViewById<Button>(Resource.Id.btnSignIn);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);


            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                SignupActivity signUpDialog = new SignupActivity();
                
                Intent intent = new Intent(Application.Context, typeof(SignupActivity));  //the activity you want to open
                this.StartActivity(intent);
            };

            mBtnLogIn.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                LoginFrgActivity logInDialog = new LoginFrgActivity();
                logInDialog.Show(transaction, "dialog fragment");
            };
        }
    }
}
