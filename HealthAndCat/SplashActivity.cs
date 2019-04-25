using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace HealthAndCat
{
    [Activity(Label = "SplashActivity",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(this, typeof(Creator)));
        }
    }
}