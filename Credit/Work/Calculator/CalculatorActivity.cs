using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Credit.Commands.Calculator;
using Credit.Work.CalculaterResult;
using Newtonsoft.Json;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Work.Calculator
{
    [Activity(Label = "Калькулятор", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class CalculatorActivity : BurgerActivity
    {
        private EditText _duration;
        private EditText _amount;
        private EditText _percent;
        private string _type;
        private Button _apply;
        protected override string SupportTitle => "Калькулятор";
        protected override int LayoutResId => Resource.Layout.calculator;

        protected override void LoadSyncElements()
        {
            base.LoadSyncElements();

            _duration = FindViewById<EditText>(Resource.Id.calculcator_duration);
            _amount = FindViewById<EditText>(Resource.Id.calculator_amount);
            _percent = FindViewById<EditText>(Resource.Id.calculator_percent);

            var type1 = FindViewById<RadioButton>(Resource.Id.radio_type1);
            type1.CheckedChange += Type1OnCheckedChange;

            var type2 = FindViewById<RadioButton>(Resource.Id.radio_type2);
            type2.CheckedChange += Type2OnCheckedChange;

            _apply = FindViewById<Button>(Resource.Id.calculator_apply);
            _apply.Click += ApplyOnClick;
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

        private void ApplyOnClick(object sender, EventArgs e)
        {
            var calculateModel = new CalculateRequest(_duration.Text, _amount.Text, _percent.Text, _type);

            ShowLoaderInMainThread();

            ThreadPool.QueueUserWorkItem(w =>
            {
                if (calculateModel.IsValid(ShowError))
                {
                    var commandDelegate = new CommandDelegate<CalculateResponce>(OnSuccess, ShowError, ShowErrorNotEnternet);
                    var command = new CalculateCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(calculateModel);
                }
                DissmissLoaderInMainThread();
            });
        }

        private void OnSuccess(CalculateResponce model)
        {
            RunOnUiThread(() =>
            {
                var intent = new Intent(this, typeof(CalculatorResultActivity));
                intent.PutExtra("credit",JsonConvert.SerializeObject(model.Credit));
                StartActivity(intent);
            });
        }
    }
}