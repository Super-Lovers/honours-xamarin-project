using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Timers;
using Android.Content;
using HealthAndCat.Resources.layout;

namespace HealthAndCat
{
    [Activity(Label = "HealthAndCat", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // The clock for the player to see the
        // time of day so he can schedule and play differently.
        public static TextView ClockView;
        private TextView _currencyView;
        public static int PlayerCurrency = 100;

        public static ImageView CatView;
        private bool _isCatClicked = false;

        private Button _storeButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            CatView = FindViewById<ImageView>(Resource.Id.imageView1);
            CatView.Click += OnClickedCat;

            _currencyView = FindViewById<TextView>(Resource.Id.textView2);
            _currencyView.Text = "Cash: " + PlayerCurrency.ToString();

            ClockView = FindViewById<TextView>(Resource.Id.textView1);
            ClockView.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            // Button for opening the Store activity
            _storeButton = FindViewById<Button>(Resource.Id.button1);
            _storeButton.Click += OnStoreClick;

            // The timer handles the text view's content after
            // every interval of milliseconds.
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(UpdateClock);
            myTimer.Interval = 1000;
            myTimer.Start();
        }

        public void OnStoreClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Store));
            StartActivity(intent);
        }
        
        public void OnClickedCat(object sender, EventArgs e)
        {
            if (_isCatClicked)
            {
                CatView.ImageAlpha = 255;
                _isCatClicked = false;
            } else
            {
                CatView.ImageAlpha = 155;
                _isCatClicked = true;
            }
        }

        private void UpdateClock(object source, ElapsedEventArgs e)
        {
            ClockView = FindViewById<TextView>(Resource.Id.textView1);
            ClockView.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }
    }
}

