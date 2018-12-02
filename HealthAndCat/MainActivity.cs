using Android.App;
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
        private ImageView _timeIcon;
        private TextView _currencyView;
        public static int PlayerCurrency = 100;
        public static int DaysSinceBeginning;

        public static ImageView CatView;
        private bool _isCatClicked = false;

        private Button _storeButton;
        private Button _inventoryButton;
        private Button _takeCatOut;
        private TextView _walkTimer;
        private TextView _mealTimer;
        private TextView _toysTimer;

        protected override void OnResume()
        {
            base.OnResume();

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

            if (_timeIcon == null)
            {
                _timeIcon = FindViewById<ImageView>(Resource.Id.imageView2);
            }

            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 12) // Morning period
            {
                _timeIcon.SetImageResource(Resource.Drawable.sunny);
            }
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 17)
            {
                _timeIcon.SetImageResource(Resource.Drawable.sunny);
            }
            else if (DateTime.Now.Hour > 17 && DateTime.Now.Hour < 21)
            {
                _timeIcon.SetImageResource(Resource.Drawable.sun);
            }
            else if (DateTime.Now.Hour >= 21 && DateTime.Now.Hour < 5)
            {
                _timeIcon.SetImageResource(Resource.Drawable.moon);
            } else
            {
                _timeIcon.SetImageResource(Resource.Drawable.moon);
            }

            _mealTimer = FindViewById<TextView>(Resource.Id.textView5);
            if (localSlaveData.Contains("Year Of Meal"))
            {
                _mealTimer.Enabled = true;
                _mealTimer.Visibility = Android.Views.ViewStates.Visible;

                // The timer handles the text view's content after
                // every interval of milliseconds.
                Timer timer = new Timer(new TimerCallback(UpdateClock));
                // First parameter is when the event starts after
                // the activity is initialized and the second parameter
                // is the interval between each code block execution
                timer.Change(1000, 1000);
            }
            else
            {
                _mealTimer.Enabled = false;
                _mealTimer.Visibility = Android.Views.ViewStates.Gone;
            }

            _toysTimer = FindViewById<TextView>(Resource.Id.textView6);
            if (localSlaveData.GetBoolean("Played With Toy", false))
            {
                _toysTimer.Enabled = true;
                _toysTimer.Visibility = Android.Views.ViewStates.Visible;

                Timer timer = new Timer(new TimerCallback(UpdateClock));
                timer.Change(1000, 1000);
            }
            else
            {
                _toysTimer.Enabled = false;
                _toysTimer.Visibility = Android.Views.ViewStates.Gone;
            }

            if (localSlaveData.Contains("TakenCatOut"))
            {
                // Depending if the player has taken out his cat out
                // recently, the button for doing that will be toggled accordingly.
                DateTime dateOfLastWalk = new DateTime(
                        localSlaveData.GetInt("Year Of Walk", 0),
                        localSlaveData.GetInt("Month Of Walk", 0),
                        localSlaveData.GetInt("Day Of Walk", 0),
                        localSlaveData.GetInt("Hour Of Walk", 0),
                        0,
                        0
                    ).AddHours(3);

                //Console.WriteLine("\n WALK AT " + dateOfLastWalk + "\n");
                //Console.WriteLine("\n" + dateOfLastWalk.Hour + "\n");
                //Console.WriteLine("\n" + DateTime.Now.Hour + "\n");

                // We want the player to only take his cat out in
                // daylight because it might be dangerous in the dark.
                if (DateTime.Now > dateOfLastWalk &&
                    DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20) // Between 8AM and 20PM
                {
                    _takeCatOut.Enabled = true;
                    _takeCatOut.Text = "Take cat out";
                }
                else
                {
                    if (DateTime.Now < dateOfLastWalk)
                    {
                        _takeCatOut.Text = "Cant go out at this time!";
                    }
                    else
                    {
                        _takeCatOut.Text = "It's too late to go out!";
                    }
                    _takeCatOut.Enabled = false;
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();
            
            _timeIcon = FindViewById<ImageView>(Resource.Id.imageView2);
            
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 12) // Morning period
            {
                _timeIcon.SetImageResource(Resource.Drawable.sunny);
            }
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 17)
            {
                _timeIcon.SetImageResource(Resource.Drawable.sunny);
            }
            else if (DateTime.Now.Hour > 17 && DateTime.Now.Hour < 21)
            {
                _timeIcon.SetImageResource(Resource.Drawable.sun);
            }
            else if (DateTime.Now.Hour >= 21 && DateTime.Now.Hour < 5)
            {
                _timeIcon.SetImageResource(Resource.Drawable.moon);
            }
            else
            {
                _timeIcon.SetImageResource(Resource.Drawable.moon);
            }

            if (localSlaveData.Contains("Year Of Play"))
            {
                _toysTimer = FindViewById<TextView>(Resource.Id.textView6);
                if (localSlaveData.GetBoolean("Played With Toy", false))
                {
                    _toysTimer.Enabled = true;
                    _toysTimer.Visibility = Android.Views.ViewStates.Visible;

                    Timer timer = new Timer(new TimerCallback(UpdateClock));
                    timer.Change(1000, 1000);
                }
                else
                {
                    _toysTimer.Enabled = false;
                    _toysTimer.Visibility = Android.Views.ViewStates.Gone;
                }
            } else
            {
                localSlaveDataEdit.PutInt("Year Of Play", DateTime.Now.Year);
                localSlaveDataEdit.PutInt("Month Of Play", DateTime.Now.Month);
                localSlaveDataEdit.PutInt("Day Of Play", DateTime.Now.Day);
                localSlaveDataEdit.PutInt("Hour Of Play", DateTime.Now.Day);

                localSlaveDataEdit.Commit();
            }

            _walkTimer = FindViewById<TextView>(Resource.Id.textView4);
            _mealTimer = FindViewById<TextView>(Resource.Id.textView5);
            if (localSlaveData.Contains("Year Of Meal"))
            {
                _mealTimer.Enabled = true;
                _mealTimer.Visibility = Android.Views.ViewStates.Visible;
                
                Timer timer = new Timer(new TimerCallback(UpdateClock));
                timer.Change(1000, 1000);
            } else
            {
                _mealTimer.Enabled = false;
                _mealTimer.Visibility = Android.Views.ViewStates.Gone;
            }

                if (localSlaveData.GetBoolean("TakenCatOut", false))
            {
                _walkTimer.Enabled = true;
                _walkTimer.Visibility = Android.Views.ViewStates.Visible;

                // The timer handles the text view's content after
                // every interval of milliseconds.
                Timer timer = new Timer(new TimerCallback(UpdateClock));
                // First parameter is when the event starts after
                // the activity is initialized and the second parameter
                // is the interval between each code block execution
                timer.Change(1000, 1000);
            }
            else
            {
                _walkTimer.Enabled = false;
                _walkTimer.Visibility = Android.Views.ViewStates.Gone;
            }

            // If the player has started the game before, then we can extract
            // his already existing profile data instead of starting a new one
            // and resetting his progress.
            if (localSlaveData.Contains("Player Cash"))
            {
                PlayerCurrency = localSlaveData.GetInt("Player Cash", 0);
            }
            else
            {
                localSlaveDataEdit.PutInt("Player Cash", 100);
                // Pushes the new file edit changes to the source file.
                localSlaveDataEdit.Commit();
                PlayerCurrency = localSlaveData.GetInt("Player Cash", 0);
            }

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

                    // Also giving the player more cash for the new day.
                    PlayerCurrency += 100;
                    localSlaveDataEdit.PutInt("Player Cash", PlayerCurrency);

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

            if (localSlaveData.Contains("Played With Toy"))
            {
                // Depending if the player has taken out his cat out
                // recently, the button for doing that will be toggled accordingly.
                DateTime dateOfLastPlay = new DateTime(
                        localSlaveData.GetInt("Year Of Play", 0),
                        localSlaveData.GetInt("Month Of Play", 0),
                        localSlaveData.GetInt("Day Of Play", 0),
                        localSlaveData.GetInt("Hour Of Play", 0),
                        0,
                        0
                    ).AddHours(6);
                
                if (DateTime.Now > dateOfLastPlay)
                {
                    localSlaveDataEdit.PutBoolean("Played With Toy", false);
                    localSlaveDataEdit.Commit();
                }
                else
                {
                    localSlaveDataEdit.PutBoolean("Played With Toy", true);
                    localSlaveDataEdit.Commit();
                }
            }
            else
            {
                localSlaveDataEdit.PutBoolean("Played With Toy", false);
            }

            _takeCatOut = FindViewById<Button>(Resource.Id.button3);
            _takeCatOut.Click += TakeCatForAWalk;
            
            if (localSlaveData.Contains("TakenCatOut"))
            {
                // Depending if the player has taken out his cat out
                // recently, the button for doing that will be toggled accordingly.
                DateTime dateOfLastWalk = new DateTime(
                        localSlaveData.GetInt("Year Of Walk", 0),
                        localSlaveData.GetInt("Month Of Walk", 0),
                        localSlaveData.GetInt("Day Of Walk", 0),
                        localSlaveData.GetInt("Hour Of Walk", 0),
                        0,
                        0
                    ).AddHours(3);

                //Console.WriteLine("\n WALK AT " + dateOfLastWalk + "\n");
                //Console.WriteLine("\n" + dateOfLastWalk.Hour + "\n");
                //Console.WriteLine("\n" + DateTime.Now.Hour + "\n");

                // We want the player to only take his cat out in
                // daylight because it might be dangerous in the dark.
                if (DateTime.Now > dateOfLastWalk &&
                    DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20) // Between 8AM and 20PM
                {
                    _takeCatOut.Enabled = true;
                    _takeCatOut.Text = "Take cat out";
                    localSlaveDataEdit.PutBoolean("TakenCatOut", false);
                    localSlaveDataEdit.Commit();
                } else
                {
                    if (DateTime.Now < dateOfLastWalk)
                    {
                        _takeCatOut.Text = "Cant go out at this time!";
                        localSlaveDataEdit.PutBoolean("TakenCatOut", true);
                    } else
                    {
                        _takeCatOut.Text = "It's too late to go out!";
                    }
                    _takeCatOut.Enabled = false;
                    localSlaveDataEdit.Commit();
                }
            } else
            {
                localSlaveDataEdit.PutBoolean("TakenCatOut", false);
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
                    localSlaveDataEdit.Commit();
                }
            }

            CatView = FindViewById<ImageView>(Resource.Id.imageView1);
            CatView.Click += OnClickedCat;

            _currencyView = FindViewById<TextView>(Resource.Id.textView2);
            _currencyView.Text = "Cash: " + PlayerCurrency;

            ClockView = FindViewById<TextView>(Resource.Id.textView1);
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 12) // Morning period
            {
                ClockView.Text = "Morning";
            } else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 17)
            {
                ClockView.Text = "Afternoon";
            } else if (DateTime.Now.Hour > 17 && DateTime.Now.Hour < 21)
            {
                ClockView.Text = "Evening";
            } else if (DateTime.Now.Hour >= 21 && DateTime.Now.Hour < 5)
            {
                ClockView.Text = "Night";
            }

            // Button for opening the Store activity
            _storeButton = FindViewById<Button>(Resource.Id.button1);
            _storeButton.Click += OnStoreClick;

            // Button for opening the Store activity
            _inventoryButton = FindViewById<Button>(Resource.Id.button2);
            _inventoryButton.Click += OnInventoryClick;
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
                var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

                DateTime dateOfLastWalk = new DateTime(
                        localSlaveData.GetInt("Year Of Walk", 0),
                        localSlaveData.GetInt("Month Of Walk", 0),
                        localSlaveData.GetInt("Day Of Walk", 0),
                        localSlaveData.GetInt("Hour Of Walk", 0),
                        0,
                        0
                    ).AddHours(3);

                DateTime dateOfLastMeal = new DateTime(
                        localSlaveData.GetInt("Year Of Meal", 0),
                        localSlaveData.GetInt("Month Of Meal", 0),
                        localSlaveData.GetInt("Day Of Meal", 0),
                        localSlaveData.GetInt("Hour Of Meal", 0),
                        0,
                        0
                    ).AddHours(6);

                DateTime dateOfLastPlay = new DateTime(
                        localSlaveData.GetInt("Year Of Play", 0),
                        localSlaveData.GetInt("Month Of Play", 0),
                        localSlaveData.GetInt("Day Of Play", 0),
                        localSlaveData.GetInt("Hour Of Play", 0),
                        0,
                        0
                    ).AddHours(6);

                TimeSpan timeUntilNextWalk = dateOfLastWalk - DateTime.Now;
                TimeSpan timeUntilNextMeal = dateOfLastMeal - DateTime.Now;
                TimeSpan timeUntilNextPlay = dateOfLastPlay - DateTime.Now;

                if (timeUntilNextWalk.Minutes <= 1)
                {
                    _walkTimer.Enabled = false;
                    _walkTimer.Visibility = Android.Views.ViewStates.Gone;
                }

                if (timeUntilNextMeal.Minutes <= 1)
                {
                    _mealTimer.Enabled = false;
                    _mealTimer.Visibility = Android.Views.ViewStates.Gone;
                }

                if (timeUntilNextPlay.Minutes <= 1)
                {
                    _toysTimer.Enabled = false;
                    _toysTimer.Visibility = Android.Views.ViewStates.Gone;
                }

                _walkTimer.Text = "Returns after: " + timeUntilNextWalk.Hours + "h " + timeUntilNextWalk.Minutes + "m";
                _mealTimer.Text = "Can eat after: " + timeUntilNextMeal.Hours + "h " + timeUntilNextMeal.Minutes + "m";
                _toysTimer.Text = "Can play after: " + timeUntilNextPlay.Hours + "h " + timeUntilNextPlay.Minutes + "m";
                //ClockView = FindViewById<TextView>(Resource.Id.textView1);
                //ClockView.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            });
        }

        private void TakeCatForAWalk(object sender, EventArgs e)
        {
            _walkTimer.Enabled = true;
            _walkTimer.Visibility = Android.Views.ViewStates.Visible;

            // The timer handles the text view's content after
            // every interval of milliseconds.
            Timer timer = new Timer(new TimerCallback(UpdateClock));
            // First parameter is when the event starts after
            // the activity is initialized and the second parameter
            // is the interval between each code block execution
            timer.Change(1000, 1000);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();

            // Saving the time and date of the cat being taken out
            // so that we can set a time interval between every
            // consecutive activity.
            localSlaveDataEdit.PutInt("Year Of Walk", DateTime.Now.Year);
            localSlaveDataEdit.PutInt("Month Of Walk", DateTime.Now.Month);
            localSlaveDataEdit.PutInt("Day Of Walk", DateTime.Now.Day);
            localSlaveDataEdit.PutInt("Hour Of Walk", DateTime.Now.Hour);

            localSlaveDataEdit.PutBoolean("TakenCatOut", true);
            localSlaveDataEdit.Commit();

            _takeCatOut.Enabled = false;
        }
    }
}

