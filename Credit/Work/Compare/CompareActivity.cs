using System.Globalization;
using Android.App;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Newtonsoft.Json;
using Shared.Models;

namespace Credit.Work.Compare
{
    [Activity(Label = "Сравнение", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class CompareActivity : ReturnActivity
    {
        protected override string SupportTitle => "Сравнение";
        protected override int LayoutResId => Resource.Layout.compare;

        private Shared.Models.Credit _credit;
        private AvialableCredit _avialableCredit;
        private TextView _bank1;
        private TextView _bank2;
        private TextView _amount1;
        private TextView _amount2;
        private TextView _percent1;
        private TextView _percent2;
        private TextView _duration1;
        private TextView _duration2;

        protected override void LoadSyncElements()
        {
            base.LoadSyncElements();

            _credit = JsonConvert.DeserializeObject<Shared.Models.Credit>(Intent.GetStringExtra("credit"));
            _avialableCredit = JsonConvert.DeserializeObject<Shared.Models.AvialableCredit>(Intent.GetStringExtra("AvialableCredit"));

            _bank1 = FindViewById<TextView>(Resource.Id.compare_bank1);
            _bank1.Text = _credit.BankName;

            _bank2 = FindViewById<TextView>(Resource.Id.compare_bank2);
            _bank2.Text = _avialableCredit.BankName;

            _amount1 = FindViewById<TextView>(Resource.Id.compare_amount1);
            _amount1.Text = _credit.Amount.ToString(new CultureInfo("ru"));

            _amount2 = FindViewById<TextView>(Resource.Id.compare_amount2);
            _amount2.Text = "до " + _avialableCredit.MaxAmount.ToString(new CultureInfo("ru"));

            _percent1 = FindViewById<TextView>(Resource.Id.compare_percent1);
            _percent1.Text = _credit.Procent.ToString(new CultureInfo("ru"));

            _percent2 = FindViewById<TextView>(Resource.Id.compare_percent2);
            _percent2.Text = _avialableCredit.Percent.ToString(new CultureInfo("ru"));

            _duration1 = FindViewById<TextView>(Resource.Id.compare_duration1);
            _duration1.Text = _credit.DurationInMonth.ToString(new CultureInfo("ru"));

            _duration2 = FindViewById<TextView>(Resource.Id.compare_duration2);
            _duration2.Text = "до " + _avialableCredit.MaxDuration.ToString(new CultureInfo("ru"));
        }
    }
}