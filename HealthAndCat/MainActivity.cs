﻿using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using HealthAndCat.Resources.layout;
using System.Threading;

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
        public static int DaysSinceBeginning;

        public static ImageView CatView;
        private bool _isCatClicked = false;

        private Button _storeButton;
        private Button _inventoryButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();

            DateTime newLoginDay;

            // Making sure the player's day interval system
            // is initialized or updated when he launches the app
            // at the start or later on.
            if (localSlaveData.Contains("Year Of Previous Login"))
            {
                newLoginDay = new DateTime(
                        localSlaveData.GetInt("Year Of Previous Login", 0),
                        localSlaveData.GetInt("Month Of Previous Login", 0),
                        localSlaveData.GetInt("Day Of Previous Login", 0)
                    ).AddDays(1);
                //Console.WriteLine("Updated Time");
            } else
            {
                localSlaveDataEdit.PutInt("Year Of Previous Login", DateTime.Now.Year);
                localSlaveDataEdit.PutInt("Month Of Previous Login", DateTime.Now.Month);
                localSlaveDataEdit.PutInt("Day Of Previous Login", DateTime.Now.Day);

                localSlaveDataEdit.Commit();

                // Initializing the counter of the start of the game.
                // (How many days the player has played the game for).
                newLoginDay = new DateTime(
                        localSlaveData.GetInt("Year Of Previous Login", 0),
                        localSlaveData.GetInt("Month Of Previous Login", 0),
                        localSlaveData.GetInt("Day Of Previous Login", 0)
                    ).AddDays(1);

                //Console.WriteLine("Initiated Time");
            }

            if (localSlaveData.Contains("Days Since Beginning"))
            {
                if (DateTime.Now > newLoginDay)
                {
                    // If the player has logged in after 24 hours then the new
                    // bonus interval is updated and the counter of total days
                    // of playing is updated as well.
                    DaysSinceBeginning = localSlaveData.GetInt("Days Since Beginning", 0);
                    DaysSinceBeginning++;
                    localSlaveDataEdit.PutInt("Days Since Beginning", DaysSinceBeginning);

                    localSlaveDataEdit.PutInt("Year Of Previous Login", DateTime.Now.Year);
                    localSlaveDataEdit.PutInt("Month Of Previous Login", DateTime.Now.Month);
                    localSlaveDataEdit.PutInt("Day Of Previous Login", DateTime.Now.Day);
                    localSlaveDataEdit.Commit();

                    //Console.WriteLine("New Time");
                } else
                {
                    DaysSinceBeginning = localSlaveData.GetInt("Days Since Beginning", 0);
                    //Console.WriteLine("Day " + DaysSinceBeginning);
                }
            } else
            {
                localSlaveDataEdit.PutInt("Days Since Beginning", 0);
                localSlaveDataEdit.Commit();
                DaysSinceBeginning = localSlaveData.GetInt("Days Since Beginning", 0);

                //Console.WriteLine("New Beginning");
            }

            TextView DayCounterView = FindViewById<TextView>(Resource.Id.textView3);
            DayCounterView.Text = "Day " + DaysSinceBeginning;

            if (localSlaveData.Contains("Year Of Meal"))
            {
                // Getting the date of the last meal and adding
                // the interval of time between the previous and next meal.
                DateTime dateOfLastMeal = new DateTime(
                        localSlaveData.GetInt("Year Of Meal", 0),
                        localSlaveData.GetInt("Month Of Meal", 0),
                        localSlaveData.GetInt("Day Of Meal", 0),
                        localSlaveData.GetInt("Hour Of Meal", 0),
                        0,
                        0
                    ).AddHours(6);

                //Console.WriteLine("NOW " + DateTime.Now);
                //Console.WriteLine("LAST MEAL " + dateOfLastMeal);

                // Checking if the interval between the meal the player had is past
                // the time until the next meal and if so, he can eat again.
                if (DateTime.Now > dateOfLastMeal)
                {
                    localSlaveDataEdit.PutBoolean("CanUseItem", true);
                }
            }
            // If the player has started the game before, then we can extract
            // his already existing profile data instead of starting a new one
            // and resetting his progress.
            if (localSlaveData.Contains("Player Cash"))
            {
                localSlaveDataEdit.PutInt("Player Cash", 100);
                // Pushes the new file edit changes to the source file.
                localSlaveDataEdit.Commit();
                PlayerCurrency = localSlaveData.GetInt("Player Cash", 0);
            } else
            {
                localSlaveDataEdit.PutInt("Player Cash", 100);
                localSlaveDataEdit.Commit();
                PlayerCurrency = localSlaveData.GetInt("Player Cash", 0);
            }

            CatView = FindViewById<ImageView>(Resource.Id.imageView1);
            CatView.Click += OnClickedCat;

            _currencyView = FindViewById<TextView>(Resource.Id.textView2);
            _currencyView.Text = "Cash: " + PlayerCurrency;

            ClockView = FindViewById<TextView>(Resource.Id.textView1);
            ClockView.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            // Button for opening the Store activity
            _storeButton = FindViewById<Button>(Resource.Id.button1);
            _storeButton.Click += OnStoreClick;

            // Button for opening the Store activity
            _inventoryButton = FindViewById<Button>(Resource.Id.button2);
            _inventoryButton.Click += OnInventoryClick;

            // The timer handles the text view's content after
            // every interval of milliseconds.
            Timer timer = new Timer(new TimerCallback(UpdateClock));
            // First parameter is when the event starts after
            // the activity is initialized and the second parameter
            // is the interval between each code block execution
            timer.Change(1000, 1000);
        }

        public void OnStoreClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Store));
            StartActivity(intent);
        }

        public void OnInventoryClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Inventory));
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

        private void UpdateClock(object source)
        {
            RunOnUiThread(() =>
            {
                ClockView = FindViewById<TextView>(Resource.Id.textView1);
                ClockView.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            });
        }
    }
}

