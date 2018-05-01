
using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;

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
        private EditText _startDate;
        private EditText _percent;
        private EditText _amount;
        private AutoCompleteTextView _type;
        private Button _apply;

        protected override void LoadSyncElements()
        {
            _name = FindViewById<EditText>(Resource.Id.create_credit_name);

            _bank = FindViewById<EditText>(Resource.Id.create_credit_bank);

            _type = FindViewById<AutoCompleteTextView>(Resource.Id.create_credit_type);
            _type.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,new List<string>(){ "Дифференцированный", "Аннуитет" });
            _type.Text = "Дифференцированный";
            _type.Threshold = 1;

            _duration = FindViewById<EditText>(Resource.Id.create_credit_duration);

            _startDate = FindViewById<EditText>(Resource.Id.create_credit_start_date);

            _percent = FindViewById<EditText>(Resource.Id.create_credit_percent);

            _amount = FindViewById<EditText>(Resource.Id.create_credit_amount);

            _apply = FindViewById<Button>(Resource.Id.create_credit_apply);
            _apply.Click += ApplyOnClick;
        }

        private void ApplyOnClick(object sender, EventArgs eventArgs)
        {
            ShowError("Rjle", "Apply click");
        }
    }
}