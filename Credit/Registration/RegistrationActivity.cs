using System;
using System.Threading;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Shared.Commands.Registration;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Registration
{
    [Activity(Label = "Регистрация", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class RegistrationActivity : ReturnActivity
    {
        protected override string SupportTitle => "Регистрация";
        protected override int LayoutResId => Resource.Layout.registration;

        private EditText _name;
        private EditText _secondName;
        private EditText _password;
        private EditText _email;
        private EditText _town;
        private Button _apply;

        protected override void LoadSyncElements()
        {
            _name = FindViewById<EditText>(Resource.Id.registration_name);

            _secondName = FindViewById<EditText>(Resource.Id.registration_second_name);

            _password = FindViewById<EditText>(Resource.Id.registration_password);

            _email = FindViewById<EditText>(Resource.Id.registration_email);

            _town = FindViewById<EditText>(Resource.Id.registration_town);

            _apply = FindViewById<Button>(Resource.Id.registratino_send);
            _apply.Click +=SendRegistration;
        }

        private void SendRegistration(object sender, EventArgs eventArgs)
        {
            HideKeyboard();
            var model = new RegistrationRequest(_name.Text, _secondName.Text, _email.Text, _password.Text, _town.Text);

            if (model.IsValid(ShowError))
            {
                var commandDelegate = new CommandDelegate<RegistrationResponce>(OnSuccessRegistration, ShowError, ShowErrorNotEnternet);
                var command = new RegistrationCommand(LocalDb.Instance, commandDelegate);
                ThreadPool.QueueUserWorkItem(w =>
                {
                    ShowLoaderInMainThread();
                    command.Execute(model);
                    DissmissLoaderInMainThread();
                });
            }
        }

        private void OnSuccessRegistration(RegistrationResponce responce)
        {
            Finish();
        }
    }
}