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
        public TextView BowlingLeisure;
        public TextView MovieLeisure;
        public TextView GolfLeisure;
        #endregion

        private List<Button> _useItemButtons = new List<Button>();

        protected override void OnResume()
        {
            base.OnResume();

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();

            DateTime dateOfLastWalk = new DateTime(
                         localSlaveData.GetInt("Year Of Walk", 0),
                         localSlaveData.GetInt("Month Of Walk", 0),
                         localSlaveData.GetInt("Day Of Walk", 0),
                         localSlaveData.GetInt("Hour Of Walk", 0),
                         0,
                         0
                     ).AddHours(3);
            
            if (DateTime.Now < dateOfLastWalk)
            {
                localSlaveDataEdit.PutBoolean("WentExercising", true);
            }
            else
            {
                localSlaveDataEdit.PutBoolean("WentExercising", false);
            }
            localSlaveDataEdit.Commit();

            if (localSlaveData.GetBoolean("WentExercising", false))
            {
                foreach (var button in _useItemButtons)
                {
                    button.Enabled = false;
                }
            } else
            {
                if (localSlaveData.GetBoolean("Had Leisure", false))
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() == "Bowling" ||
                            button.Tag.ToString() == "Movie" ||
                            button.Tag.ToString() == "Golf")
                            button.Enabled = false;
                    }
                }
                else
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() == "Bowling" ||
                            button.Tag.ToString() == "Movie" ||
                            button.Tag.ToString() == "Golf")
                            button.Enabled = true;
                    }
                }

                if (localSlaveData.GetBoolean("CanUseItem", false) == false)
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() != "Bowling" &&
                            button.Tag.ToString() != "Movie" &&
                            button.Tag.ToString() != "Golf")
                            button.Enabled = false;
                    }
                }
                else
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() != "Bowling" &&
                            button.Tag.ToString() != "Movie" &&
                            button.Tag.ToString() != "Golf")
                            button.Enabled = true;
                    }
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Inventory);

            // Creating a new file with custom name and access level
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
            OatsLabel.Text = "Oats: " + HealthAndCat.Resources.layout.Store.Oats.ToString();

            if (localSlaveData.Contains("Cheese Food"))
            {
                HealthAndCat.Resources.layout.Store.Cheese = localSlaveData.GetInt("Cheese Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Cheese = 0;
            }
            CheeseLabel = FindViewById<TextView>(Resource.Id.Cheese);
            CheeseLabel.Text = "Cheese: " + HealthAndCat.Resources.layout.Store.Cheese.ToString();

            if (localSlaveData.Contains("Egg Food"))
            {
                HealthAndCat.Resources.layout.Store.Eggs = localSlaveData.GetInt("Egg Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Eggs = 0;
            }
            EggsLabel = FindViewById<TextView>(Resource.Id.Eggs);
            EggsLabel.Text = "Eggs: " + HealthAndCat.Resources.layout.Store.Eggs.ToString();

            if (localSlaveData.Contains("Chicken Food"))
            {
                HealthAndCat.Resources.layout.Store.Chicken = localSlaveData.GetInt("Chicken Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Chicken = 0;
            }
            ChickenLabel = FindViewById<TextView>(Resource.Id.Chicken);
            ChickenLabel.Text = "Chicken: " + HealthAndCat.Resources.layout.Store.Chicken.ToString();

            if (localSlaveData.Contains("Fish Food"))
            {
                HealthAndCat.Resources.layout.Store.Fish = localSlaveData.GetInt("Fish Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Fish = 0;
            }
            FishLabel = FindViewById<TextView>(Resource.Id.Fish);
            FishLabel.Text = "Fish: " + HealthAndCat.Resources.layout.Store.Fish.ToString();

            if (localSlaveData.Contains("Turkey Food"))
            {
                HealthAndCat.Resources.layout.Store.Turkey = localSlaveData.GetInt("Turkey Food", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Turkey = 0;
            }
            TurkeyLabel = FindViewById<TextView>(Resource.Id.Turkey);
            TurkeyLabel.Text = "Turkey: " + HealthAndCat.Resources.layout.Store.Turkey.ToString();
            #endregion

            #region Toy views in store - Page 2
            if (localSlaveData.Contains("Bowlings"))
            {
                HealthAndCat.Resources.layout.Store.Bowlings = localSlaveData.GetInt("Bowlings", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Bowlings = 0;
            }
            BowlingLeisure = FindViewById<TextView>(Resource.Id.Bowling);
            BowlingLeisure.Text = "Bowling: " + HealthAndCat.Resources.layout.Store.Bowlings.ToString();

            if (localSlaveData.Contains("Movies"))
            {
                HealthAndCat.Resources.layout.Store.Movies = localSlaveData.GetInt("Movies", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Movies = 0;
            }
            MovieLeisure = FindViewById<TextView>(Resource.Id.Movies);
            MovieLeisure.Text = "Movie: " + HealthAndCat.Resources.layout.Store.Movies.ToString();

            if (localSlaveData.Contains("Golf"))
            {
                HealthAndCat.Resources.layout.Store.Golf = localSlaveData.GetInt("Golf", 0);
            }
            else
            {
                HealthAndCat.Resources.layout.Store.Golf = 0;
            }
            GolfLeisure = FindViewById<TextView>(Resource.Id.Golf);
            GolfLeisure.Text = "Golf: " + HealthAndCat.Resources.layout.Store.Golf.ToString();
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

            // Editing that new file with the new variable that uses editing mode.
            var localSlaveDataEdit = localSlaveData.Edit();

            DateTime dateOfLastWalk = new DateTime(
                         localSlaveData.GetInt("Year Of Walk", 0),
                         localSlaveData.GetInt("Month Of Walk", 0),
                         localSlaveData.GetInt("Day Of Walk", 0),
                         localSlaveData.GetInt("Hour Of Walk", 0),
                         0,
                         0
                     ).AddHours(3);
            
            if (DateTime.Now < dateOfLastWalk)
            {
                localSlaveDataEdit.PutBoolean("WentExercising", true);
            } else
            {
                localSlaveDataEdit.PutBoolean("WentExercising", false);
            }

            localSlaveDataEdit.Commit();

            if (localSlaveData.GetBoolean("WentExercising", false))
            {
                foreach (var button in _useItemButtons)
                {
                    button.Enabled = false;
                }
            }
            else
            {
                if (localSlaveData.GetBoolean("Had Leisure", false))
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() == "Bowling" ||
                            button.Tag.ToString() == "Movie" ||
                            button.Tag.ToString() == "Golf")
                            button.Enabled = false;
                    }
                }
                else
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() == "Bowling" ||
                            button.Tag.ToString() == "Movie" ||
                            button.Tag.ToString() == "Golf")
                            button.Enabled = true;
                    }
                }

                if (localSlaveData.GetBoolean("CanUseItem", false) == false)
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() != "Bowling" &&
                            button.Tag.ToString() != "Movie" &&
                            button.Tag.ToString() != "Golf")
                            button.Enabled = false;
                    }
                }
                else
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() != "Bowling" &&
                            button.Tag.ToString() != "Movie" &&
                            button.Tag.ToString() != "Golf")
                            button.Enabled = true;
                    }
                }
            }
            #endregion

            localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
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
            var localSlaveDataEdit = localSlaveData.Edit();

            //if (localSlaveData.GetBoolean("CanUseItem", false) == true)
            //{
                switch (buttonClicked.Tag.ToString())
                {
                    // ******
                    // Leisures
                    // ******
                    case "Bowling":
                        if (HealthAndCat.Resources.layout.Store.Bowlings > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Bowlings--;
                            CommitIntToStorage("Bowlings", HealthAndCat.Resources.layout.Store.Bowlings);

                            BowlingLeisure.Text = "Bowlings: " + localSlaveData.GetInt("Bowlings", 0).ToString();

                            localSlaveDataEdit.PutInt("Year Of Play", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Play", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Play", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Play", DateTime.Now.Hour);
                            
                            localSlaveDataEdit.PutBoolean("Had Leisure", true);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Movie":
                        if (HealthAndCat.Resources.layout.Store.Movies > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Movies--;
                            CommitIntToStorage("Movies", HealthAndCat.Resources.layout.Store.Movies);

                            MovieLeisure.Text = "Movies: " + HealthAndCat.Resources.layout.Store.Movies.ToString();

                            localSlaveDataEdit.PutInt("Year Of Play", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Play", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Play", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Play", DateTime.Now.Hour);

                            localSlaveDataEdit.PutBoolean("Had Leisure", true);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Golf":
                        if (HealthAndCat.Resources.layout.Store.Golf > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Golf--;
                            CommitIntToStorage("Golf", HealthAndCat.Resources.layout.Store.Golf);

                            GolfLeisure.Text = "Golf: " + HealthAndCat.Resources.layout.Store.Golf.ToString();

                            localSlaveDataEdit.PutInt("Year Of Play", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Play", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Play", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Play", DateTime.Now.Hour);

                            localSlaveDataEdit.PutBoolean("Had Leisure", true);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    // ******
                    // Foods
                    // ******
                    case "Oats":
                        if (HealthAndCat.Resources.layout.Store.Oats > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Oats--;
                            OatsLabel.Text = "Oats: " + HealthAndCat.Resources.layout.Store.Oats.ToString();

                            CommitIntToStorage("Oats Food", HealthAndCat.Resources.layout.Store.Oats);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Cheese":
                        if (HealthAndCat.Resources.layout.Store.Cheese > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Cheese--;
                            CheeseLabel.Text = "Chees: " + HealthAndCat.Resources.layout.Store.Cheese.ToString();

                            CommitIntToStorage("Cheese Food", HealthAndCat.Resources.layout.Store.Cheese);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Egg":
                        if (HealthAndCat.Resources.layout.Store.Eggs > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Eggs--;
                            EggsLabel.Text = "Eggs: " + HealthAndCat.Resources.layout.Store.Eggs.ToString();

                            CommitIntToStorage("Egg Food", HealthAndCat.Resources.layout.Store.Eggs);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Chicken":
                        if (HealthAndCat.Resources.layout.Store.Chicken > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Chicken--;
                            ChickenLabel.Text = "Chicken: " + HealthAndCat.Resources.layout.Store.Chicken.ToString();

                            CommitIntToStorage("Chicken Food", HealthAndCat.Resources.layout.Store.Chicken);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Fish":
                        if (HealthAndCat.Resources.layout.Store.Fish > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Fish--;
                            FishLabel.Text = "Fish: " + HealthAndCat.Resources.layout.Store.Fish.ToString();

                            CommitIntToStorage("Fish Food", HealthAndCat.Resources.layout.Store.Fish);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                    case "Turkey":
                        if (HealthAndCat.Resources.layout.Store.Turkey > 0)
                        {
                            HealthAndCat.Resources.layout.Store.Turkey--;
                            TurkeyLabel.Text = "Turkey: " + HealthAndCat.Resources.layout.Store.Turkey.ToString();

                            CommitIntToStorage("Turkey Food", HealthAndCat.Resources.layout.Store.Turkey);

                            localSlaveDataEdit.PutBoolean("CanUseItem", false);
                            localSlaveDataEdit.PutInt("Year Of Meal", DateTime.Now.Year);
                            localSlaveDataEdit.PutInt("Month Of Meal", DateTime.Now.Month);
                            localSlaveDataEdit.PutInt("Day Of Meal", DateTime.Now.Day);
                            localSlaveDataEdit.PutInt("Hour Of Meal", DateTime.Now.Hour);
                            localSlaveDataEdit.Commit();
                        }
                        break;
                }

                if (localSlaveData.GetBoolean("Had Leisure", false))
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() == "Bowling" ||
                            button.Tag.ToString() == "Movie" ||
                            button.Tag.ToString() == "Golf")
                            button.Enabled = false;
                    }
                }

                if (localSlaveData.GetBoolean("CanUseItem", false) == false)
                {
                    foreach (var button in _useItemButtons)
                    {
                        if (button.Tag.ToString() != "Bowling" &&
                            button.Tag.ToString() != "Movie" &&
                            button.Tag.ToString() != "Golf")
                            button.Enabled = false;
                    }
                }
            //}
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