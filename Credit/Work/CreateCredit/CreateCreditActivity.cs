using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Credit.Work.ListCredit;
using Shared.Commands.CreateCredit;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Work.CreateCredit
{
    [Activity(Label = "Создать кредит", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class CreateCreditActivity : ReturnActivity
    {
        protected override string SupportTitle => "Создать кредит";
        protected override int LayoutResId => Resource.Layout.create_credit;

        private EditText _name;
        private EditText _bank;
        private EditText _duration;
        private Button _startDate;
        private EditText _percent;
        private EditText _amount;
        private string _type;
        private Button _apply;

        //TODO проверка при создании credit
        // валидация на типы и числа
        // отправка запроса на оплату
        //Работа оплаты только в онлайн
        // обновление списки после оплаты
        protected override void LoadSyncElements()
        {
            _name = FindViewById<EditText>(Resource.Id.create_credit_name);

            _bank = FindViewById<EditText>(Resource.Id.create_credit_bank);

            _duration = FindViewById<EditText>(Resource.Id.create_credit_duration);

            _startDate = FindViewById<Button>(Resource.Id.create_credit_start_date);
            _startDate.Click += StartDateOnClick;

            _percent = FindViewById<EditText>(Resource.Id.create_credit_percent);

            _amount = FindViewById<EditText>(Resource.Id.create_credit_amount);

            _apply = FindViewById<Button>(Resource.Id.create_credit_apply);
            _apply.Click += ApplyOnClick;


            var type1 = FindViewById<RadioButton>(Resource.Id.radio_type1);
            type1.CheckedChange += Type1OnCheckedChange;

            var type2 = FindViewById<RadioButton>(Resource.Id.radio_type2);
            type2.CheckedChange += Type2OnCheckedChange;
        }

        private void Type2OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            if (checkedChangeEventArgs.IsChecked)
            {
                _type = "Аннуитет";
            }
        }

        private void Type1OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            if (checkedChangeEventArgs.IsChecked)
            {
                _type = "Дифференцированный";
            }
        }

        private void StartDateOnClick(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;
            var datePickerDialog = new DatePickerDialog(this, (o, args) =>
            {
                _startDate.Text = new DateTime(args.Year, args.Month, args.DayOfMonth).ToString("d", new CultureInfo("ru"));
            },now.Year, now.Month, now.Day);

            datePickerDialog.Show();
        }

        private void ApplyOnClick(object sender, EventArgs eventArgs)
        {
            var model = new CreateCreditRequestModel(_name.Text, _bank.Text, _type, _percent.Text, _duration.Text, _startDate.Text, _amount.Text);
            if (model.IsValid(ShowError))
            {
                ThreadPool.QueueUserWorkItem(w =>
                {
                    ShowLoaderInMainThread();
                    var commandDelegate = new CommandDelegate<CreateCreditResponce>(OnSuccess, ShowError, ShowErrorNotEnternet);
                    var command = new CreateCreditCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(model);
                    DissmissLoaderInMainThread();
                });
            }
        }

        private void OnSuccess(CreateCreditResponce responce)
        {
            RunOnUiThread(() =>
            {
                FinishAffinity();
                StartActivity(typeof(ListCreditActivity));
            });
        }
    }
}