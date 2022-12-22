using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace DriveXamarin.Resources.layout
{
    [Obsolete]
    public class ExamResultsFrg : DialogFragment
    {
        TextView lblScoreDetails;
        Button btnCloseResults;
        int score;
        
        public static int _finishPoints;
        public static HttpClient client = new HttpClient();
        private int statusCode = 0;
        public static readonly string url = "http://10.0.2.2:5000/driveapi/user/Users/Profile";//http://10.0.2.2:5000/driveapi/user/Users/Profile


        [Obsolete]
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        [Obsolete]
        public override void OnDismiss(IDialogInterface dialog)
        {//Dismiss device after completion
            base.OnDismiss(dialog);
            Activity activity = this.Activity;
            ((IDialogInterfaceOnDismissListener)activity).OnDismiss(dialog);
        }
        public static async Task<string> GetToken()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + LoginFrgActivity.accessToken);
            Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    {"points", $"{_finishPoints}" }
                };
            
            var serializedContent = JsonConvert.SerializeObject(dict);
            var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, stringContent);
            var resultContent = await result.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(resultContent);
            return responseObject.points;
        }

        [Obsolete]
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var resultDisplayFrg = inflater.Inflate(Resource.Layout.activity_exam_result, container, false);
            score = Arguments.GetInt("Счёт", 56); //Impossibly high value entered for debugging purposes, maximum attainable score is 5
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            lblScoreDetails = resultDisplayFrg.FindViewById<TextView>(Resource.Id.lblScoreDetails);
            string resultdetails;
            if (ExamActivity._points == 3)
            {
                resultdetails = $"Вы заработали {ExamActivity._points} балла";
                _finishPoints = MyProfileActivity._currentPoints + ExamActivity._points;
                GetToken();
            }
            if (ExamActivity._points == 2)
            {
                resultdetails = $"Вы заработали {ExamActivity._points} балла";
                _finishPoints = MyProfileActivity._currentPoints + ExamActivity._points;
                GetToken();
            }
            if (ExamActivity._points == 1)
            {
                resultdetails = $"Вы заработали {ExamActivity._points} балла";
                _finishPoints = MyProfileActivity._currentPoints + ExamActivity._points;
                GetToken();
            }
            else resultdetails = $"Вы провалили экзамен!"; ;
            lblScoreDetails.Text = resultdetails; //Display results

            //Restrict user from using background click feature so only on button press will fragment end
            this.Dialog.SetCanceledOnTouchOutside(false);

            btnCloseResults = resultDisplayFrg.FindViewById<Button>(Resource.Id.btnCloseResults);
            btnCloseResults.Click += (object sender, EventArgs e) =>
            {
                Dismiss();
            };

            return resultDisplayFrg;
        }

        [Obsolete]
        public override void OnActivityCreated(Bundle savedInstanceState)
        {//Implement animation
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.quizAnimation;

        }

    }
}
