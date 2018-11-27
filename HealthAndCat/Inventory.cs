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

            toggleFoodAndToys = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            toggleFoodAndToys.Click += ToggleFoodAndToys;

            #region Food views in store - Page 1
            OatsLabel = FindViewById<TextView>(Resource.Id.Oats);
            OatsLabel.Text = HealthAndCat.Resources.layout.Store.Oats.ToString();

            CheeseLabel = FindViewById<TextView>(Resource.Id.Cheese);
            CheeseLabel.Text = HealthAndCat.Resources.layout.Store.Cheese.ToString();

            EggsLabel = FindViewById<TextView>(Resource.Id.Eggs);
            EggsLabel.Text = HealthAndCat.Resources.layout.Store.Eggs.ToString();

            ChickenLabel = FindViewById<TextView>(Resource.Id.Chicken);
            ChickenLabel.Text = HealthAndCat.Resources.layout.Store.Chicken.ToString();

            FishLabel = FindViewById<TextView>(Resource.Id.Fish);
            FishLabel.Text = HealthAndCat.Resources.layout.Store.Fish.ToString();

            TurkeyLabel = FindViewById<TextView>(Resource.Id.Turkey);
            TurkeyLabel.Text = HealthAndCat.Resources.layout.Store.Turkey.ToString();
            #endregion

            #region Toy views in store - Page 2
            CastleToy = FindViewById<TextView>(Resource.Id.CastleToy);
            CastleToy.Text = HealthAndCat.Resources.layout.Store.CastleToys.ToString();

            BallToy = FindViewById<TextView>(Resource.Id.BallToy);
            BallToy.Text = HealthAndCat.Resources.layout.Store.BallToys.ToString();

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
            #endregion

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

            switch (buttonClicked.Tag.ToString())
            {
                // ******
                // Toys
                // ******
                case "Castle Toy":
                    if (HealthAndCat.Resources.layout.Store.CastleToys > 0)
                    {
                        HealthAndCat.Resources.layout.Store.CastleToys--;
                        CastleToy.Text = HealthAndCat.Resources.layout.Store.CastleToys.ToString();
                    }
                    break;
                case "Ball Toy":
                    if (HealthAndCat.Resources.layout.Store.BallToys > 0)
                    {
                        HealthAndCat.Resources.layout.Store.BallToys--;
                        BallToy.Text = HealthAndCat.Resources.layout.Store.BallToys.ToString();
                    }
                    break;
                case "Mouse Toy":
                    if (HealthAndCat.Resources.layout.Store.MouseToys > 0)
                    {
                        HealthAndCat.Resources.layout.Store.MouseToys--;
                        MouseToy.Text = HealthAndCat.Resources.layout.Store.MouseToys.ToString();
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
                    }
                    break;
                case "Cheese":
                    if (HealthAndCat.Resources.layout.Store.Cheese > 0)
                    {
                        HealthAndCat.Resources.layout.Store.Cheese--;
                        CheeseLabel.Text = HealthAndCat.Resources.layout.Store.Cheese.ToString();
                    }
                    break;
                case "Egg":
                    if (HealthAndCat.Resources.layout.Store.Eggs > 0)
                    {
                        HealthAndCat.Resources.layout.Store.Eggs--;
                        EggsLabel.Text = HealthAndCat.Resources.layout.Store.Eggs.ToString();
                    }
                    break;
                case "Chicken":
                    if (HealthAndCat.Resources.layout.Store.Chicken > 0)
                    {
                        HealthAndCat.Resources.layout.Store.Chicken--;
                        ChickenLabel.Text = HealthAndCat.Resources.layout.Store.Chicken.ToString();
                    }
                    break;
                case "Fish":
                    if (HealthAndCat.Resources.layout.Store.Fish > 0)
                    {
                        HealthAndCat.Resources.layout.Store.Fish--;
                        FishLabel.Text = HealthAndCat.Resources.layout.Store.Fish.ToString();
                    }
                    break;
                case "Turkey":
                    if (HealthAndCat.Resources.layout.Store.Turkey > 0)
                    {
                        HealthAndCat.Resources.layout.Store.Turkey--;
                        TurkeyLabel.Text = HealthAndCat.Resources.layout.Store.Turkey.ToString();
                    }
                    break;
            }
        }
    }
}