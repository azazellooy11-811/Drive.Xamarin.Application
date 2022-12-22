
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
using DriveXamarin.Resources.layout;
using FFImageLoading;
using Newtonsoft.Json;

namespace DriveXamarin
{
    [Activity(Label = "ErrorQuestionActivity")]
    public class ErrorQuestionActivity : Activity
    {
        private Button _btnSubmitAnswer;
        private TextView _lblQuestionCount;
        private TextView _lblScore;

        private List<Question> _allQuestions;
        private RadioGroup _radioAnswerGroup;
        private RadioButton _rbAnswer1;
        private RadioButton _rbAnswer2;
        private RadioButton _rbAnswer3;
        private RadioButton _rbAnswer4;
        private TextView _txtQuestion;
        private TextView _txt;
        private ImageView _imageQuestion;
        List<Question> _chosenList;
        List<Question> _addedQuestions;

        private int _correctCount, _inCorrectCount, _inCorrectCountTwo, _questionCount = 1;
        private int questionstoask = 20;
        private bool errorInAddQuestions = false;
        public static int _points;
        Question Question = new Question();
        public static Question ErrorQuestion = new Question();
        Question _currentQuestion = new Question();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_error_question);
            var client = new HttpClient();
            var resultJson = await client.GetAsync("http://10.0.2.2:5000/driveapi/Questions/List");//http://10.0.2.2:5000/driveapi/Questions/List
            List<Question> resultQuestion = JsonConvert.DeserializeObject<List<Question>>(await resultJson.Content.ReadAsStringAsync());

            
            _chosenList = Question.PullXRandomQuestions(resultQuestion, 20);
            Question = _chosenList[0];

            #region
            _txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            _rbAnswer1 = FindViewById<RadioButton>(Resource.Id.radioAnswer1);
            _rbAnswer2 = FindViewById<RadioButton>(Resource.Id.radioAnswer2);
            _rbAnswer3 = FindViewById<RadioButton>(Resource.Id.radioAnswer3);
            _rbAnswer4 = FindViewById<RadioButton>(Resource.Id.radioAnswer4);
            _imageQuestion = FindViewById<ImageView>(Resource.Id.imageQuestion);
            _txt = FindViewById<TextView>(Resource.Id.txt);
            _radioAnswerGroup = FindViewById<RadioGroup>(Resource.Id.radioAnswerGroup);
            //_lblQuestionCount = FindViewById<TextView>(Resource.Id.lblQuestionCount);
            //_lblScore = FindViewById<TextView>(Resource.Id.lblScore);
            _btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            _imageQuestion = FindViewById<ImageView>(Resource.Id.imageQuestion);
            #endregion
            var image = ImageService.Instance.LoadUrl(Question.File.name);
            image.Into(_imageQuestion);
            _txtQuestion.Text = Question.Text;
            _txt.Text = Question.Prompt;
            UseButtons(Question);
            _btnSubmitAnswer.Click += BtnSubmitAnswer_Click;
        }
        private void BtnSubmitAnswer_Click(object sender, EventArgs e)
        {
            //Prohibit user from continuing one radio button is selected
            if (_rbAnswer1.Checked || _rbAnswer2.Checked || _rbAnswer3.Checked || _rbAnswer4.Checked)
            {
                //Find which radio button has been selected
                var SelectedButton = FindViewById<RadioButton>(_radioAnswerGroup.CheckedRadioButtonId);
                if (Question.IsAnswerCorrect(SelectedButton.Text, Question))
                {
                    //Correct Answer has been selected Display message and play sound
                    var ansSelect = Toast.MakeText(this, "Правильно", ToastLength.Short);
                    ansSelect.Show();
                    _correctCount++;
                }
                else
                {
                    var ansSelect = Toast.MakeText(this, "Ошибка", ToastLength.Short);
                    ansSelect.Show();
                    _inCorrectCount++;
                    ErrorQuestion = Question;
                    GetErrorQuestion();
                }

                NextQuestion(); //Regardless of answer selected, fill field with new question details
            }
            else
            {
                //if no radio button is selected inform user
                var ansSelect = Toast.MakeText(this, "Пожалуйста, выберите ответ", ToastLength.Short);
                ansSelect.Show();
            }

        }
        public static async Task<string> GetErrorQuestion()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + LoginFrgActivity.accessToken);
            //var serializedContent1 = JsonConvert.SerializeObject(ErrorQuestion);
            //Dictionary<string, string> errorQuestion = new Dictionary<string, string>()
            //    {
            //        {"id", $"{ErrorQuestion.Id}" }
            //    };

            var url = "http://10.0.2.2:5000/driveapi/user/Users/ErrorQuestions";
            var serializedContent = JsonConvert.SerializeObject(ErrorQuestion);
            var stringContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, stringContent);
            var resultContent = await result.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(resultContent);
            return responseObject.errorQuestion;
        }
        [Obsolete]
        private void NextQuestion()
        {
            //If the user has not yet completed the quiz show the next answer
            if (_questionCount < questionstoask)
            {
                _radioAnswerGroup.ClearCheck();
                _questionCount++;
                Question = _chosenList[_questionCount - 1];
                var image = ImageService.Instance.LoadUrl(Question.File.name);
                image.Into(_imageQuestion);
                _txtQuestion.Text = Question.Text;
                _txt.Text = Question.Prompt;
                UseButtons(Question);
                //_lblQuestionCount.Text = $"Вопрос: {_questionCount}/{questionstoask}";
                //_lblScore.Text = $"Счёт: {_correctCount}";
            } //Otherwise call function end quiz
            else
            {
                FinishQuiz();
            }
        }
        [Obsolete]
        private void FinishQuiz()
        {//Play sound on completion and display results fragment
            FragmentTransaction finishtxn = FragmentManager.BeginTransaction();
            var questionData = new Bundle();
            ExamResultsFrg quizresults = new ExamResultsFrg() { Arguments = questionData };
            

        }

        //void IDialogInterfaceOnDismissListener.OnDismiss(IDialogInterface dialog)
        //{//On Dismiss of the fragment Finish activity
        //    Finish();
        //}


        private void UseButtons(Question Question)
        {
            var countAnsw = Question.Answers.Count;
            _rbAnswer1.Text = $"{Question.Answers[0].Text}";
            _rbAnswer2.Text = $"{Question.Answers[1].Text}";
            if (countAnsw < 3)
            {
                _rbAnswer3.Visibility = Android.Views.ViewStates.Invisible;

            }
            else
            {
                _rbAnswer3.Text = $"{Question.Answers[2].Text}";
            }
            if (countAnsw < 4)
            {
                _rbAnswer4.Visibility = Android.Views.ViewStates.Invisible;
            }
            else
            {
                _rbAnswer4.Text = $"{Question.Answers[3].Text}";
            }
        }



    }
}
