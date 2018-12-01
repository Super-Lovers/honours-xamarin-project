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

namespace HealthAndCat
{
    [Activity(Label = "Inventory")]
    public class Inventory : Activity
    {
        public ToggleButton toggleFoodAndToys;
        public LinearLayout FoodsLayout;
        public LinearLayout ToysLayout;
        private bool _toggleTab;

        #region Food values in store - Page 1
        public TextView OatsLabel;
        public TextView CheeseLabel;
        public TextView EggsLabel;
        public TextView ChickenLabel;
        public TextView FishLabel;
        public TextView TurkeyLabel;
        #endregion

        #region Toy values in store - Page 2
        public TextView CastleToy;
        public TextView BallToy;
        public TextView MouseToy;
        #endregion

        private List<Button> _useItemButtons = new List<Button>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Inventory);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

            toggleFoodAndToys = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            toggleFoodAndToys.Click += ToggleFoodAndToys;

            #region Food views in store - Page 1
            if (localSlaveData.Contains("Oats Food"))
            {
                HealthAndCat.Resources.layout.Store.Oats = localSlaveData.GetInt("Oats Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Oats = 0;
            }
            OatsLabel = FindViewById<TextView>(Resource.Id.Oats);
            OatsLabel.Text = HealthAndCat.Resources.layout.Store.Oats.ToString();

            if (localSlaveData.Contains("Cheese Food"))
            {
                HealthAndCat.Resources.layout.Store.Cheese = localSlaveData.GetInt("Cheese Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Cheese = 0;
            }
            CheeseLabel = FindViewById<TextView>(Resource.Id.Cheese);
            CheeseLabel.Text = HealthAndCat.Resources.layout.Store.Cheese.ToString();

            if (localSlaveData.Contains("Egg Food"))
            {
                HealthAndCat.Resources.layout.Store.Eggs = localSlaveData.GetInt("Egg Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Eggs = 0;
            }
            EggsLabel = FindViewById<TextView>(Resource.Id.Eggs);
            EggsLabel.Text = HealthAndCat.Resources.layout.Store.Eggs.ToString();

            if (localSlaveData.Contains("Chicken Food"))
            {
                HealthAndCat.Resources.layout.Store.Chicken = localSlaveData.GetInt("Chicken Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Chicken = 0;
            }
            ChickenLabel = FindViewById<TextView>(Resource.Id.Chicken);
            ChickenLabel.Text = HealthAndCat.Resources.layout.Store.Chicken.ToString();

            if (localSlaveData.Contains("Fish Food"))
            {
                HealthAndCat.Resources.layout.Store.Fish = localSlaveData.GetInt("Fish Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Fish = 0;
            }
            FishLabel = FindViewById<TextView>(Resource.Id.Fish);
            FishLabel.Text = HealthAndCat.Resources.layout.Store.Fish.ToString();

            if (localSlaveData.Contains("Turkey Food"))
            {
                HealthAndCat.Resources.layout.Store.Turkey = localSlaveData.GetInt("Turkey Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Turkey = 0;
            }
            TurkeyLabel = FindViewById<TextView>(Resource.Id.Turkey);
            TurkeyLabel.Text = HealthAndCat.Resources.layout.Store.Turkey.ToString();
            #endregion

            #region Toy views in store - Page 2
            if (localSlaveData.Contains("Castle Toys"))
            {
                HealthAndCat.Resources.layout.Store.CastleToys = localSlaveData.GetInt("Castle Toys", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.CastleToys = 0;
            }
            CastleToy = FindViewById<TextView>(Resource.Id.CastleToy);
            CastleToy.Text = HealthAndCat.Resources.layout.Store.CastleToys.ToString();

            if (localSlaveData.Contains("Ball Toys"))
            {
                HealthAndCat.Resources.layout.Store.BallToys = localSlaveData.GetInt("Ball Toys", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.BallToys = 0;
            }
            BallToy = FindViewById<TextView>(Resource.Id.BallToy);
            BallToy.Text = HealthAndCat.Resources.layout.Store.BallToys.ToString();

            if (localSlaveData.Contains("Mouse Toys"))
            {
                HealthAndCat.Resources.layout.Store.MouseToys = localSlaveData.GetInt("Mouse Toys", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.MouseToys = 0;
            }
            MouseToy = FindViewById<TextView>(Resource.Id.MouseToy);
            MouseToy.Text = HealthAndCat.Resources.layout.Store.MouseToys.ToString();
            #endregion

            #region Buttons for buying/selling Food/Toys
            Button use1 = FindViewById<Button>(Resource.Id.Use1);
            _useItemButtons.Add(use1);

            Button use2 = FindViewById<Button>(Resource.Id.Use2);
            _useItemButtons.Add(use2);

            Button use3 = FindViewById<Button>(Resource.Id.Use3);
            _useItemButtons.Add(use3);

            Button use4 = FindViewById<Button>(Resource.Id.Use4);
            _useItemButtons.Add(use4);

            Button use5 = FindViewById<Button>(Resource.Id.Use5);
            _useItemButtons.Add(use5);

            Button use6 = FindViewById<Button>(Resource.Id.Use6);
            _useItemButtons.Add(use6);

            Button use7 = FindViewById<Button>(Resource.Id.Use7);
            _useItemButtons.Add(use7);

            Button use8 = FindViewById<Button>(Resource.Id.Use8);
            _useItemButtons.Add(use8);

            Button use9 = FindViewById<Button>(Resource.Id.Use9);
            _useItemButtons.Add(use9);

            // Making the buttons for buying and selling items interactable
            foreach (var button in _useItemButtons)
            {
                button.Click += UseItem;
            }
            
            if (localSlaveData.GetBoolean("CanUseItem", false) == false)
            {
                foreach (var button in _useItemButtons)
                {
                    button.Enabled = false;
                }
            }
            else
            {
                foreach (var button in _useItemButtons)
                {
                    button.Enabled = true;
                }
            }
            #endregion


            // Creating a new file with custom name and access level
            localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            // and then editing that file with the new variable that uses editing mode.
            var localSlaveDataEdit = localSlaveData.Edit();
            if (localSlaveData.Contains("CanUseItem") == false)
            {
                localSlaveDataEdit.PutBoolean("CanUseItem", true);
            }
            localSlaveDataEdit.Commit();

            FoodsLayout = FindViewById<LinearLayout>(Resource.Id.Foods);
            FoodsLayout.Visibility = ViewStates.Visible;
            ToysLayout = FindViewById<LinearLayout>(Resource.Id.Toys);
            ToysLayout.Visibility = ViewStates.Gone;
        }

        private void ToggleFoodAndToys(object sender, EventArgs e)
        {
            if (_toggleTab)
            {
                FoodsLayout.Visibility = ViewStates.Visible;
                ToysLayout.Visibility = ViewStates.Gone;
                _toggleTab = false;
            }
            else
            {
                FoodsLayout.Visibility = ViewStates.Gone;
                ToysLayout.Visibility = ViewStates.Visible;
                _toggleTab = true;
            }
        }

        private void UseItem(object sender, EventArgs e)
        {
            Button buttonClicked = (Button)sender;
            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

            if (localSlaveData.GetBoolean("CanUseItem", false) == true)
            {
                switch (buttonClicked.Tag.ToString())
                {
                    // ******
                    // Toys
                    // ******
                    case "Castle Toy":
                        if (HealthAndCat.Resources.layout.Store.CastleToys > 0)
                        {
                            HealthAndCat.Resources.layout.Store.CastleToys--;
                            CastleToy.Text = localSlaveData.GetInt("Castle Toys", 0).ToString();

                            CommitIntToStorage("Castle Toys", HealthAndCat.Resources.layout.Store.CastleToys);
                        }
                        break;
                    case "Ball Toy":
                        if (HealthAndCat.Resources.layout.Store.BallToys > 0)
                        {
                            HealthAndCat.Resources.layout.Store.BallToys--;
                            BallToy.Text = HealthAndCat.Resources.layout.Store.BallToys.ToString();

                            CommitIntToStorage("Ball Toys", HealthAndCat.Resources.layout.Store.BallToys);
                        }
                        break;
                    case "Mouse Toy":
                        if (HealthAndCat.Resources.layout.Store.MouseToys > 0)
                        {
                            HealthAndCat.Resources.layout.Store.MouseToys--;
                            MouseToy.Text = HealthAndCat.Resources.layout.Store.MouseToys.ToString();

                            CommitIntToStorage("Mouse Toys", HealthAndCat.Resources.layout.Store.MouseToys);
                        }
                        break;
                    // ******
                    // Foods
                    // ******
                    case "Oats":
                        if (HealthAndCat.Resources.layout.Store.Oats > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Oats--;
                            OatsLabel.Text = HealthAndCat.Resources.layout.Store.Oats.ToString();

                            CommitIntToStorage("Oats Food", HealthAndCat.Resources.layout.Store.Oats);
                        }
                        break;
                    case "Cheese":
                        if (HealthAndCat.Resources.layout.Store.Cheese > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Cheese--;
                            CheeseLabel.Text = HealthAndCat.Resources.layout.Store.Cheese.ToString();

                            CommitIntToStorage("Cheese Food", HealthAndCat.Resources.layout.Store.Cheese);
                        }
                        break;
                    case "Egg":
                        if (HealthAndCat.Resources.layout.Store.Eggs > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Eggs--;
                            EggsLabel.Text = HealthAndCat.Resources.layout.Store.Eggs.ToString();

                            CommitIntToStorage("Egg Food", HealthAndCat.Resources.layout.Store.Eggs);
                        }
                        break;
                    case "Chicken":
                        if (HealthAndCat.Resources.layout.Store.Chicken > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Chicken--;
                            ChickenLabel.Text = HealthAndCat.Resources.layout.Store.Chicken.ToString();

                            CommitIntToStorage("Chicken Food", HealthAndCat.Resources.layout.Store.Chicken);
                        }
                        break;
                    case "Fish":
                        if (HealthAndCat.Resources.layout.Store.Fish > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Fish--;
                            FishLabel.Text = HealthAndCat.Resources.layout.Store.Fish.ToString();

                            CommitIntToStorage("Fish Food", HealthAndCat.Resources.layout.Store.Fish);
                        }
                        break;
                    case "Turkey":
                        if (HealthAndCat.Resources.layout.Store.Turkey > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Turkey--;
                            TurkeyLabel.Text = HealthAndCat.Resources.layout.Store.Turkey.ToString();

                            CommitIntToStorage("Turkey Food", HealthAndCat.Resources.layout.Store.Turkey);
                        }
                        break;
                }
                var localSlaveDataEdit = localSlaveData.Edit();
                localSlaveDataEdit.PutBoolean("CanUseItem", false);
                localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                localSlaveDataEdit.Commit();

                foreach (var button in _useItemButtons)
                {
                    button.Enabled = false;
                }
            }
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