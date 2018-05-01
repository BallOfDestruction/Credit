using Android.App;
using Android.OS;
using Credit.Work.ListCredit;
using Credit.Work.Login;
using Shared.Database;
using Shared.Models;

namespace Credit.Work.Splash
{
    [Activity(Label = "Credit", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var user = LocalDb.Instance.GetFirstItem<LocalUserModel>();
            Finish();
            StartActivity(user == null ? typeof(LoginActivity) : typeof(ListCreditActivity));
        }
    }
}