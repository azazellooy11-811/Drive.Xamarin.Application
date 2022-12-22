using System;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using DriveXamarin.Models;
using Google.Android.Material.BottomNavigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace DriveXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private TextView textMessage;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.navigation_rules)
            {
                Intent intent = new Intent(this, typeof(RulesActivity));  //the activity you want to open
                this.StartActivity(intent);
            }
            if (id == Resource.Id.navigation_education)
            {
                Intent intent = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                this.StartActivity(intent);
            }
            if (id == Resource.Id.navigation_exam)
            {
                Intent intent = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                this.StartActivity(intent);
            }
            return false;
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var _myProfileBtn = FindViewById<ImageButton>(Resource.Id.profileImageBtn);
            var _errorQBtn = FindViewById<Button>(Resource.Id.errors);
            var _settingsButton = FindViewById<ImageButton>(Resource.Id.settingImageButton);
            _myProfileBtn.Click += ProfileImageBtn_Click;
            _errorQBtn.Click += ErrorQuest_Click;
            _settingsButton.Click += SettingsActivityOpen;
            var navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

        }
        private void SettingsActivityOpen(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(SettingsActivity));
            this.StartActivity(intent);

        }
        private  void ProfileImageBtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(MyProfileActivity));
            this.StartActivity(intent);

        }
        private void ErrorQuest_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(ErrorQuestionActivity));
            this.StartActivity(intent);

        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
        //    [GeneratedEnum] Permission[] grantResults)
        //{
        //    Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}