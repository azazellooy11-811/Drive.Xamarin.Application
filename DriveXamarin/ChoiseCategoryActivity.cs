
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

namespace DriveXamarin
{
    [Activity(Label = "ChoiseCategoryActivity")]
    public class ChoiseCategoryActivity : Activity
    {
        public Button abButton;
        public Button cdButton;
        public static string driveCategory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_choiseDC);

            abButton = FindViewById<Button>(Resource.Id.AB);
            cdButton = FindViewById<Button>(Resource.Id.CD);
            abButton.Click += Click_AB;
            cdButton.Click += Click_CD;
        }
        private void Click_AB(object sender, EventArgs e)
        {
            driveCategory = "AB";
            if (driveCategory == "AB")
            {
                Toast.MakeText(Application.Context, "Выбрана категория AB", ToastLength.Short).Show();
            }

        }
        private void Click_CD(object sender, EventArgs e)
        {
            driveCategory = "CD";
            if (driveCategory == "CD")
            {
                Toast.MakeText(Application.Context, "Выбрана категория CD", ToastLength.Short).Show();
            }
        }
    }
}
