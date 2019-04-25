using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace HealthAndCat.Resources.layout
{
    [Activity(Label = "Store", Theme = "@android:style/Theme.Material")]
    public class Store : Activity
    {
        public ToggleButton toggleFoodAndToys;
        public LinearLayout FoodsLayout;
        public LinearLayout LeisureLayout;
        private bool _toggleTab;
        private TextView _currentCash;

        #region Food values in store - Page 1
        //public ImageView OatsView;
        public TextView OatsLabel;
        private int OatsFoodCost = 20;
        public static int Oats = 0;
        
        //public ImageView CheeseView;
        public TextView CheeseLabel;
        private int CheeseFoodCost = 20;
        public static int Cheese = 0;

        //public ImageView EggsView;
        public TextView EggsLabel;
        private int EggsFoodCost = 20;
        public static int Eggs = 0;

        //public ImageView ChickenView;
        public TextView ChickenLabel;
        private int ChickenFoodCost = 20;
        public static int Chicken = 0;

        //public ImageView FishView;
        public TextView FishLabel;
        private int FishFoodCost = 20;
        public static int Fish = 0;

        //public ImageView TurkeyView;
        public TextView TurkeyLabel;
        private int TurkeyFoodCost = 20;
        public static int Turkey = 0;
        #endregion

        #region Toy values in store - Page 2
        public TextView BowlingLeisure;
        private int BowlingCost = 10;
        public static int Bowlings = 0;

        public TextView MovieLeisure;
        private int MovieCost = 20;
        public static int Movies = 0;

        public TextView GolfLeisure;
        private int GolfCost = 40;
        public static int Golf = 0;
        #endregion

        private List<Button> _buyButtons = new List<Button>();
        private List<Button> _sellButtons = new List<Button>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Store);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

            _currentCash = FindViewById<TextView>(Resource.Id.textView2);
            _currentCash.Text = "Cash: $" + localSlaveData.GetInt("Player Cash", 0);

            toggleFoodAndToys = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            toggleFoodAndToys.Click += ToggleFoodAndToys;

            #region Food views in store - Page 1
            OatsLabel = FindViewById<TextView>(Resource.Id.Oats);
            OatsLabel.Text = "Oats - $" + OatsFoodCost.ToString();

            CheeseLabel = FindViewById<TextView>(Resource.Id.Cheese);
            CheeseLabel.Text = "Cheese - $" + CheeseFoodCost.ToString();

            EggsLabel = FindViewById<TextView>(Resource.Id.Eggs);
            EggsLabel.Text = "Egg - $" + EggsFoodCost.ToString();

            ChickenLabel = FindViewById<TextView>(Resource.Id.Chicken);
            ChickenLabel.Text = "Chicken - $" + ChickenFoodCost.ToString();

            FishLabel = FindViewById<TextView>(Resource.Id.Fish);
            FishLabel.Text = "Fish - $" + ChickenFoodCost.ToString();

            TurkeyLabel = FindViewById<TextView>(Resource.Id.Turkey);
            TurkeyLabel.Text = "Turkey - $" + TurkeyFoodCost.ToString();
            #endregion

            #region Toy views in store - Page 2
            BowlingLeisure = FindViewById<TextView>(Resource.Id.BowlingText);
            BowlingLeisure.Text = "Bowling - $" + MovieCost.ToString();

            MovieLeisure = FindViewById<TextView>(Resource.Id.MovieText);
            MovieLeisure.Text = "Movie - $" + MovieCost.ToString();

            GolfLeisure = FindViewById<TextView>(Resource.Id.GolfText);
            GolfLeisure.Text = "Golf - $" + GolfCost.ToString();
            #endregion

            #region Buttons for buying/selling Food/Toys
            Button buy1 = FindViewById<Button>(Resource.Id.Buy1);
            Button sell1 = FindViewById<Button>(Resource.Id.Sell1);
            _buyButtons.Add(buy1);
            _sellButtons.Add(sell1);

            Button buy2 = FindViewById<Button>(Resource.Id.Buy2);
            Button sell2 = FindViewById<Button>(Resource.Id.Sell2);
            _buyButtons.Add(buy2);
            _sellButtons.Add(sell2);

            Button buy3 = FindViewById<Button>(Resource.Id.Buy3);
            Button sell3 = FindViewById<Button>(Resource.Id.Sell3);
            _buyButtons.Add(buy3);
            _sellButtons.Add(sell3);

            Button buy4 = FindViewById<Button>(Resource.Id.Buy4);
            Button sell4 = FindViewById<Button>(Resource.Id.Sell4);
            _buyButtons.Add(buy4);
            _sellButtons.Add(sell4);

            Button buy5 = FindViewById<Button>(Resource.Id.Buy5);
            Button sell5 = FindViewById<Button>(Resource.Id.Sell5);
            _buyButtons.Add(buy5);
            _sellButtons.Add(sell5);

            Button buy6 = FindViewById<Button>(Resource.Id.Buy6);
            Button sell6 = FindViewById<Button>(Resource.Id.Sell6);
            _buyButtons.Add(buy6);
            _sellButtons.Add(sell6);

            Button buy7 = FindViewById<Button>(Resource.Id.Buy7);
            Button sell7 = FindViewById<Button>(Resource.Id.Sell7);
            _buyButtons.Add(buy7);
            _sellButtons.Add(sell7);

            Button buy8 = FindViewById<Button>(Resource.Id.Buy8);
            Button sell8 = FindViewById<Button>(Resource.Id.Sell8);
            _buyButtons.Add(buy8);
            _sellButtons.Add(sell8);

            Button buy9 = FindViewById<Button>(Resource.Id.Buy9);
            Button sell9 = FindViewById<Button>(Resource.Id.Sell9);
            _buyButtons.Add(buy9);
            _sellButtons.Add(sell9);

            // Making the buttons for buying and selling items interactable
            foreach (var button in _buyButtons)
            {
                button.Click += CreateTransaction;
            }
            foreach (var button in _sellButtons)
            {
                button.Click += CreateTransaction;
            }
            #endregion

            // First we retrieve all the views AND THEN
            // we update their visibility
            FoodsLayout = FindViewById<LinearLayout>(Resource.Id.Foods);
            FoodsLayout.Visibility = ViewStates.Visible;
            LeisureLayout = FindViewById<LinearLayout>(Resource.Id.Toys);
            LeisureLayout.Visibility = ViewStates.Gone;

            ImageButton titleBackButton = FindViewById<ImageButton>(Resource.Id.imageButton1);
            titleBackButton.Click += delegate
            {
                OnBackPressed();
            };
        }

        // When the button on top of the store interface is
        // clicked, the food tab and the toys tab with items
        // to make transactions with are toggled.
        private void ToggleFoodAndToys(object sender, EventArgs e)
        {
            if (_toggleTab)
            {
                FoodsLayout.Visibility = ViewStates.Visible;
                LeisureLayout.Visibility = ViewStates.Gone;
                _toggleTab = false;
            } else
            {
                FoodsLayout.Visibility = ViewStates.Gone;
                LeisureLayout.Visibility = ViewStates.Visible;
                _toggleTab = true;
            }
        }

        private void CreateTransaction(object sender, EventArgs e)
        {
            // We are getting the object of the button clicked
            // and converting it to an actual button like using the
            // id of buttons so that I can distinct it.
            Button buttonClicked = (Button)sender;
            if (buttonClicked.Text == "Buy")
            {
                switch (buttonClicked.Tag.ToString())
                {
                    // ******
                    // Toys
                    // ******
                    case "Bowling":
                        if (MainActivity.PlayerCurrency >= BowlingCost)
                        {
                            MainActivity.PlayerCurrency -= BowlingCost;
                            Bowlings += 1;

                            CommitIntToStorage("Bowlings", Bowlings);
                        }
                        break;
                    case "Movie":
                        if (MainActivity.PlayerCurrency >= MovieCost)
                        {
                            MainActivity.PlayerCurrency -= MovieCost;
                            Movies += 1;

                            CommitIntToStorage("Movies", Movies);
                        }
                        break;
                    case "Golf":
                        if (MainActivity.PlayerCurrency >= GolfCost)
                        {
                            MainActivity.PlayerCurrency -= GolfCost;
                            Golf += 1;

                            CommitIntToStorage("Golf", Golf);
                        }
                        break;
                    // ******
                    // Foods
                    // ******
                    case "Oats":
                        if (MainActivity.PlayerCurrency >= OatsFoodCost)
                        {
                            MainActivity.PlayerCurrency -= OatsFoodCost;
                            Oats += 1;

                            CommitIntToStorage("Oats Food", Oats);
                        }
                        break;
                    case "Cheese":
                        if (MainActivity.PlayerCurrency >= CheeseFoodCost)
                        {
                            MainActivity.PlayerCurrency -= CheeseFoodCost;
                            Cheese += 1;

                            CommitIntToStorage("Cheese Food", Cheese);
                        }
                        break;
                    case "Egg":
                        if (MainActivity.PlayerCurrency >= EggsFoodCost)
                        {
                            MainActivity.PlayerCurrency -= EggsFoodCost;
                            Eggs += 1;

                            CommitIntToStorage("Egg Food", Eggs);
                        }
                        break;
                    case "Chicken":
                        if (MainActivity.PlayerCurrency >= ChickenFoodCost)
                        {
                            MainActivity.PlayerCurrency -= ChickenFoodCost;
                            Chicken += 1;

                            CommitIntToStorage("Chicken Food", Chicken);
                        }
                        break;
                    case "Fish":
                        if (MainActivity.PlayerCurrency >= FishFoodCost)
                        {
                            MainActivity.PlayerCurrency -= FishFoodCost;
                            Fish += 1;

                            CommitIntToStorage("Fish Food", Fish);
                        }
                        break;
                    case "Turkey":
                        if (MainActivity.PlayerCurrency >= TurkeyFoodCost)
                        {
                            MainActivity.PlayerCurrency -= TurkeyFoodCost;
                            Turkey += 1;

                            CommitIntToStorage("Turkey Food", Turkey);
                        }
                        break;
                }
            } else if (buttonClicked.Text == "Sell")
            {
                switch (buttonClicked.Tag.ToString())
                {
                    // ******
                    // Leisures
                    // ******
                    case "Bowling":
                        if (Bowlings > 0)
                        {
                            MainActivity.PlayerCurrency += BowlingCost;
                            Bowlings -= 1;

                            CommitIntToStorage("Bowlings", Bowlings);
                        }
                        break;
                    case "Movie":
                        if (Movies > 0)
                        {
                            MainActivity.PlayerCurrency += MovieCost;
                            Movies -= 1;

                            CommitIntToStorage("Movies", Movies);
                        }
                        break;
                    case "Golf":
                        if (Golf > 0)
                        {
                            MainActivity.PlayerCurrency += GolfCost;
                            Golf -= 1;

                            CommitIntToStorage("Golf", Golf);
                        }
                        break;
                    // ******
                    // Foods
                    // ******
                    case "Oats":
                        if (Oats > 0)
                        {
                            MainActivity.PlayerCurrency += OatsFoodCost;
                            Oats -= 1;

                            CommitIntToStorage("Oats Food", Oats);
                        }
                        break;
                    case "Cheese":
                        if (Cheese > 0)
                        {
                            MainActivity.PlayerCurrency += CheeseFoodCost;
                            Cheese -= 1;

                            CommitIntToStorage("Cheese Food", Cheese);
                        }
                        break;
                    case "Egg":
                        if (Eggs > 0)
                        {
                            MainActivity.PlayerCurrency += EggsFoodCost;
                            Eggs -= 1;

                            CommitIntToStorage("Egg Food", Eggs);
                        }
                        break;
                    case "Chicken":
                        if (Chicken > 0)
                        {
                            MainActivity.PlayerCurrency += ChickenFoodCost;
                            Chicken -= 1;

                            CommitIntToStorage("Chicken Food", Chicken);
                        }
                        break;
                    case "Fish":
                        if (Fish > 0)
                        {
                            MainActivity.PlayerCurrency += FishFoodCost;
                            Fish -= 1;

                            CommitIntToStorage("Fish Food", Fish);
                        }
                        break;
                    case "Turkey":
                        if (Turkey > 0)
                        {
                            MainActivity.PlayerCurrency += TurkeyFoodCost;
                            Turkey -= 1;

                            CommitIntToStorage("Turkey Food", Turkey);
                        }
                        break;
                }
            }

            // Updating the player's cash after he makes a transaction.
            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();
            localSlaveDataEdit.PutInt("Player Cash", MainActivity.PlayerCurrency);
            localSlaveDataEdit.Commit();
            _currentCash.Text = "Cash: $" + MainActivity.PlayerCurrency.ToString();
        }

        public void CommitIntToStorage(string key, int value)
        {
            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();
            localSlaveDataEdit.PutInt(key, value);
            // Pushes the new file edit changes to the source file.
            localSlaveDataEdit.Commit();
        }
    }
}