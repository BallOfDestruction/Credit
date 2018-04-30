using System.Linq;
using Android;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;

namespace Credit.Base
{
    public class AccessActivity : AppCompatActivity
    {
        private const int RequestCode = 9332;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SendPermission();
        }

        private void SendPermission()
        {
            var allNeedPermission = new[]
            {
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.Internet,
                Manifest.Permission.CallPhone,
                Manifest.Permission.ProcessOutgoingCalls,
                Manifest.Permission.ReadPhoneState,
                Manifest.Permission.WriteExternalStorage,
                Manifest.Permission.AccessNetworkState,
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.AccessWifiState,
                Manifest.Permission.ChangeWifiState,
            };

            var denyied = allNeedPermission.Where(w => ContextCompat.CheckSelfPermission(this, w) != Permission.Granted).ToArray();

            if (denyied.Length != 0)
                ActivityCompat.RequestPermissions(this, denyied, RequestCode);
        }
    }
}