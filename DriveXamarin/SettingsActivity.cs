using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace DriveXamarin
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private Button _btnChoiceDriveCategory;
        private Button _btnUpdatePassword;
        private Button _btnAboutApplication;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_settings);

            _btnChoiceDriveCategory = FindViewById<Button>(Resource.Id.driveCategory);
            _btnUpdatePassword = FindViewById<Button>(Resource.Id.updatePassword);
            _btnAboutApplication = FindViewById<Button>(Resource.Id.about);

            _btnChoiceDriveCategory.Click += ChoiceDriveCategoryActivity;
            _btnUpdatePassword.Click += UpdatePasswordActivity;
            _btnAboutApplication.Click += AboutApplicationActivity;


        }
        private void ChoiceDriveCategoryActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(ChoiseCategoryActivity));
            this.StartActivity(intent);

        }
        private void UpdatePasswordActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(UpdatePasswordActivity));
            this.StartActivity(intent);

        }
        private void AboutApplicationActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(Application.Context, typeof(AboutActivity));
            this.StartActivity(intent);

        }
    }
}
