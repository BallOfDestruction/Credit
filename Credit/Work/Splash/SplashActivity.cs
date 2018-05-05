using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Credit.Work.ListCredit;
using Credit.Work.Login;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Shared.Database;
using Shared.Models;

namespace Credit.Work.Splash
{
    [Activity(Label = "Credit", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Microsoft.AppCenter.AppCenter.Start("e9ffcf21-cae4-47f2-95b9-5dc9aab4de2c", typeof(Analytics), typeof(Crashes));

            base.OnCreate(savedInstanceState);

            var allNeedPermission = new[]
            {
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage,
            };

            var denyied = allNeedPermission.Where(w => ContextCompat.CheckSelfPermission(this, w) != Permission.Granted).ToArray();

            if (denyied.Any())
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
                Finish();
            }

            var user = LocalDb.Instance.GetFirstItem<LocalUserModel>();
            var intent = new Intent(this, user == null ? typeof(LoginActivity) : typeof(ListCreditActivity));
            StartActivity(intent);
            Finish();
        }
    }
}