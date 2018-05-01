using System;
using System.Threading;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Credit.Work.ListCredit;
using Credit.Work.Registration;
using Shared.Commands.Login;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Work.Login
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
        private Button _registration;

        protected override void LoadSyncElements()
        {
            _login = FindViewById<EditText>(Resource.Id.login_email);

            _password = FindViewById<EditText>(Resource.Id.login_password);

            _send = FindViewById<Button>(Resource.Id.login_send);
            _send.Click += TryLogin;

            _registration = FindViewById<Button>(Resource.Id.login_registration);
            _registration.Click += RegistrationOnClick;
        }

        private void RegistrationOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(RegistrationActivity));
        }

        private void TryLogin(object sender, EventArgs eventArgs)
        {
            HideKeyboard();

            var model = new LoginViewModel(_login.Text, _password.Text);
            if (!model.IsValid(ShowError)) return;

            var commandDelegate = new CommandDelegate<LoginResponceViewModel>(OnSuccesLogin, ShowError, ShowErrorNotEnternet);
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
            StartActivity(typeof(ListCreditActivity));
        }
    }
}