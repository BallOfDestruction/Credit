using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Credit.Login;

namespace Credit.Splash
{
    [Activity(Label = "Credit", MainLauncher = true, Theme ="@style/Theme.AppCompat.Light")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(LoginActivity));
        }
    }
}