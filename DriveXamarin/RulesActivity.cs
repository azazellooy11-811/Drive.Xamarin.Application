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
using DriveXamarin.RulesIntent;

namespace DriveXamarin
{
    [Obsolete]
    [Activity(Label = "RulesActivity", MainLauncher = false)]
    public class RulesActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.activity_rules, items);
            //ListAdapter = new HomeScreenAdapter(this, items);
            ListView.TextFilterEnabled = true;

            //ListView.Click += OnListItemClick;
            //delegate(object sender, AdapterView.ItemClickEventArgs args)
            //{
            //    var t = items[position];
            //    Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            //};

        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = items[position];
            Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
            switch (t)
            {
                case "1. Общие положения":
                    Intent intent = new Intent(this, typeof(ZeroActivity));  //the activity you want to open
                    this.StartActivity(intent);
                    break;
                case "2. Общие обязанности водителей":
                    Intent intent1 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent1);
                    break;
                case "3. Применение специальных сигналов":
                    Intent intent2 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent2);
                    break;
                case "4. Обязанности пешеходов":
                    Intent intent3 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent3);
                    break;
                case "5. Обязанности пассажиров":
                    Intent intent4 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent4);
                    break;
                case "6. Сигналы светофора и регулировщика":
                    Intent intent5 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent5);
                    break;
                case "7. Применение аварийной сигнализации и знака аварийной остановки":
                    Intent intent6 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent6);
                    break;
                case "8. Начало движения, маневрирование":
                    Intent intent7 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent7);
                    break;
                case "9. Расположение транспортных средств на проезжей части":
                    Intent intent8 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent8);
                    break;
                case "10. Скорость движения":
                    Intent intent9 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent9);
                    break;
                case "11. Обгон, опережение, встречный разъезд":
                    Intent intent10 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent10);
                    break;
                case "12. Остановка и стоянка":
                    Intent intent11 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent11);
                    break;
                case "13. Проезд перекрестков":
                    Intent intent12 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent12);
                    break;
                case "14. Пешеходные переходы и места остановок маршрутных транспортных средств":
                    Intent intent13 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent13);
                    break;
                case "15. Движение через железнодорожные пути":
                    Intent intent14 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent14);
                    break;
                case "16. Движение по автомагистралям":
                    Intent intent15 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent15);
                    break;
                case "17. Движение в жилых зонах":
                    Intent intent16 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent16);
                    break;
                case "18. Приоритет маршрутных транспортных средств":
                    Intent intent17 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent17);
                    break;
                case "19. Пользование внешними световыми приборами и звуковыми сигналами":
                    Intent intent18 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent18);
                    break;
                case "20. Буксировка механических транспортных средств":
                    Intent intent19 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent19);
                    break;
                case "21. Учебная езда":
                    Intent intent20 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent20);
                    break;
                case "22. Перевозка людей":
                    Intent intent21 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent21);
                    break;
                case "23. Перевозка грузов":
                    Intent intent22 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent22);
                    break;
                case "24. Дополнительные требования к движению велосипедистов и водителей мопедов":
                    Intent intent23 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent23);
                    break;
                case "25. Дополнительные требования к движению гужевых повозок, а также к прогону животных":
                    Intent intent24 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent24);
                    break;
                case "26. Нормы времени управления транспортным средством и отдыха":
                    Intent intent25 = new Intent(this, typeof(ExamActivity));  //the activity you want to open
                    this.StartActivity(intent25);
                    break;

            }
        }
        static readonly string[] items = new String[]
        {
            "1. Общие положения",
            "2. Общие обязанности водителей",
            "3. Применение специальных сигналов",
            "4. Обязанности пешеходов",
            "5. Обязанности пассажиров",
            "6. Сигналы светофора и регулировщика",
            "7. Применение аварийной сигнализации и знака аварийной остановки",
            "8. Начало движения, маневрирование",
            "9. Расположение транспортных средств на проезжей части",
           "10. Скорость движения",
           "11. Обгон, опережение, встречный разъезд",
           "12. Остановка и стоянка",
           "13. Проезд перекрестков",
           "14. Пешеходные переходы и места остановок маршрутных транспортных средств",
           "15. Движение через железнодорожные пути",
           "16. Движение по автомагистралям",
           "17. Движение в жилых зонах",
           "18. Приоритет маршрутных транспортных средств",
           "19. Пользование внешними световыми приборами и звуковыми сигналами",
           "20. Буксировка механических транспортных средств",
           "21. Учебная езда",
           "22. Перевозка людей",
           "23. Перевозка грузов",
           "24. Дополнительные требования к движению велосипедистов и водителей мопедов",
           "25. Дополнительные требования к движению гужевых повозок, а также к прогону животных",
           "26. Нормы времени управления транспортным средством и отдыха"
        };



    }
}
