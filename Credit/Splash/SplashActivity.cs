using Android.App;
using Android.OS;
using Credit.ListCredit;
using Credit.Login;
using Shared.Database;
using Shared.Models;

namespace Credit.Splash
{
    [Activity(Label = "Credit", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var user = LocalDb.Instance.GetFirstItem<LocalUserModel>();

            StartActivity(user == null ? typeof(LoginActivity) : typeof(ListCreditActivity));
        }
    }
}