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

namespace HealthAndCat.Resources.layout
{
    [Activity(Label = "Store")]
    public class Store : Activity
    {
        public ToggleButton toggleFoodAndToys;
        public LinearLayout FoodsLayout;
        public LinearLayout ToysLayout;
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
        public TextView CastleToy;
        private int CastleToyCost = 10;
        public static int CastleToys = 0;

        public TextView BallToy;
        private int BallToyCost = 20;
        public static int BallToys = 0;

        public TextView MouseToy;
        private int MouseToyCost = 40;
        public static int MouseToys = 0;
        #endregion

        private List<Button> _buyButtons = new List<Button>();
        private List<Button> _sellButtons = new List<Button>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
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
            CastleToy = FindViewById<TextView>(Resource.Id.CastleToy);
            CastleToy.Text = "Castle - $" + BallToyCost.ToString();

            BallToy = FindViewById<TextView>(Resource.Id.BallToy);
            BallToy.Text = "Ball - $" + BallToyCost.ToString();

            MouseToy = FindViewById<TextView>(Resource.Id.MouseToy);
            MouseToy.Text = "Mouse - $" + MouseToyCost.ToString();
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
            ToysLayout = FindViewById<LinearLayout>(Resource.Id.Toys);
            ToysLayout.Visibility = ViewStates.Gone;
        }

        // When the button on top of the store interface is
        // clicked, the food tab and the toys tab with items
        // to make transactions with are toggled.
        private void ToggleFoodAndToys(object sender, EventArgs e)
        {
            if (_toggleTab)
            {
                FoodsLayout.Visibility = ViewStates.Visible;
                ToysLayout.Visibility = ViewStates.Gone;
                _toggleTab = false;
            } else
            {
                FoodsLayout.Visibility = ViewStates.Gone;
                ToysLayout.Visibility = ViewStates.Visible;
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
                    case "Castle Toy":
                        if (MainActivity.PlayerCurrency >= CastleToyCost)
                        {
                            MainActivity.PlayerCurrency -= CastleToyCost;
                            CastleToys += 1;

                            CommitIntToStorage("Castle Toys", CastleToys);
                        }
                        break;
                    case "Ball Toy":
                        if (MainActivity.PlayerCurrency >= BallToyCost)
                        {
                            MainActivity.PlayerCurrency -= BallToyCost;
                            BallToys += 1;

                            CommitIntToStorage("Ball Toys", BallToys);
                        }
                        break;
                    case "Mouse Toy":
                        if (MainActivity.PlayerCurrency >= MouseToyCost)
                        {
                            MainActivity.PlayerCurrency -= MouseToyCost;
                            MouseToys += 1;

                            CommitIntToStorage("Mouse Toys", MouseToys);
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
                    // Toys
                    // ******
                    case "Castle Toy":
                        if (CastleToys > 0)
                        {
                            MainActivity.PlayerCurrency += CastleToyCost;
                            CastleToys -= 1;

                            CommitIntToStorage("Castle Toys", CastleToys);
                        }
                        break;
                    case "Ball Toy":
                        if (BallToys > 0)
                        {
                            MainActivity.PlayerCurrency += BallToyCost;
                            BallToys -= 1;

                            CommitIntToStorage("Ball Toys", BallToys);
                        }
                        break;
                    case "Mouse Toy":
                        if (MouseToys > 0)
                        {
                            MainActivity.PlayerCurrency += MouseToyCost;
                            MouseToys -= 1;

                            CommitIntToStorage("Mouse Toys", MouseToys);
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