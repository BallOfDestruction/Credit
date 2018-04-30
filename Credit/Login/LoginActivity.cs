using System;
using System.Threading;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Shared.Commands.Login;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Login
{
    [Activity(Label = "Авторизация", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class LoginActivity : BaseActivity
    {
        protected override string SupportTitle => "";
        protected override int LayoutResId => Resource.Layout.login;
        protected override int? LeftButtonId => null;

        private EditText _login;
        private EditText _password;
        private Button _send;

        protected override void LoadSyncElements()
        {
            _login = FindViewById<EditText>(Resource.Id.login_email);
            _password = FindViewById<EditText>(Resource.Id.login_password);
            _send = FindViewById<Button>(Resource.Id.login_send);
            _send.Click += TryLogin;
        }

        private void TryLogin(object sender, EventArgs eventArgs)
        {
            var commandDelegate = new CommandDelegate<LoginResponceViewModel>(OnSuccesLogin, ShowError, ShowErrorNotEnternet);
            var model = new LoginViewModel(_login.Text, _password.Text);
            var command = new LoginCommand(LocalDb.Instance, commandDelegate);

            ThreadPool.QueueUserWorkItem(w =>
            {
                ShowLoaderInMainThread();
                command.Execute(model);
                DissmissLoaderInMainThread();
            });
        }

        private void OnSuccesLogin(LoginResponceViewModel responce)
        {
            Finish();
            StartActivity(typeof(ListCredit.ListCreditActivity));
        }
    }
}