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
    [Activity(Label = "Creator", MainLauncher = true)]
    public class Creator : Activity
    {
        public EditText nameOfCharacter;
        public Button startButton;
        public RadioButton radioButton1;
        public RadioButton radioButton2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Creator);

            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);

            // If the player has already registered his new character into
            // the game, then we dont want him to go through the character
            // creation process again
            if (localSlaveData.GetString("Character Name", null) == null &&
                localSlaveData.GetString("Character Gender", null) == null)
            {
                nameOfCharacter = FindViewById<EditText>(Resource.Id.editText1);
                startButton = FindViewById<Button>(Resource.Id.button1);
                radioButton1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
                radioButton2 = FindViewById<RadioButton>(Resource.Id.radioButton2);

                startButton.Click += StartGameButton;
            } else
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }

        private void StartGameButton(object sender, EventArgs e)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            var localSlaveData = GetSharedPreferences("SlaveData", FileCreationMode.Private);
            var localSlaveDataEdit = localSlaveData.Edit();

            radioButtons.Add(radioButton1);
            radioButtons.Add(radioButton2);

            localSlaveDataEdit.PutString("Character Name", nameOfCharacter.Text);

            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Tag.ToString() == "Male" &&
                    radioButton.Checked)
                {
                    localSlaveDataEdit.PutString("Character Gender", "Male");
                } else if (radioButton.Tag.ToString() == "Female" &&
                    radioButton.Checked)
                {
                    localSlaveDataEdit.PutString("Character Gender", "Female");
                }
            }

            localSlaveDataEdit.Commit();

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}