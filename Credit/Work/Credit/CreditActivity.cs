using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Newtonsoft.Json;
using Shared.Models;

namespace Credit.Work.Credit
{
    [Activity(Label = "Кредит", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class CreditActivity : ReturnActivity
    {
        protected override string SupportTitle => "Кредит";
        protected override int LayoutResId => Resource.Layout.credit;

        private Shared.Models.Credit _credit;
        private List<PaymentModel> _payments;

        private TextView _bank;
        private TextView _type;
        private TextView _duration;
        private TextView _startDate;
        private TextView _percent;
        private TextView _amount;
        private NestedScrollView _nestedScrollView;

        private RecyclerView _paymentView;

        protected override void LoadSyncElements()
        {
            var local = Intent.GetStringExtra("idCredit");
            _credit = JsonConvert.DeserializeObject<Shared.Models.Credit>(local);
            _payments = JsonConvert.DeserializeObject<List<PaymentModel>>(_credit.ListPayment ?? "");

            SupportActionBar.Title = _credit.Name;

            _bank = FindViewById<TextView>(Resource.Id.credit_bank);
            _bank.Text = _credit.BankName;

            _type = FindViewById<TextView>(Resource.Id.credit_type);
            _type.Text = _credit.TypeCredit;

            _duration = FindViewById<TextView>(Resource.Id.credit_duration);
            _duration.Text = _credit.DurationInMonth.ToString();

            _startDate = FindViewById<TextView>(Resource.Id.credit_start_date);
            _startDate.Text = _credit.StartCredit.ToString("d", new CultureInfo("ru"));

            _percent = FindViewById<TextView>(Resource.Id.credit_percent);
            _percent.Text = _credit.Procent.ToString();

            _amount = FindViewById<TextView>(Resource.Id.credit_amount);
            _amount.Text = _credit.Amount.ToString();

            _paymentView = FindViewById<RecyclerView>(Resource.Id.credit_payment);
            _paymentView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            var layoutManager = new NotCanScrollGridLayoutManager(this, 1);
            _paymentView.SetLayoutManager(layoutManager);
            _paymentView.NestedScrollingEnabled = false;

            if (_payments?.Any() == true)
            {
                _paymentView.SetAdapter(new PaymentAdapter(_payments, _credit.ServerId, ShowLoaderInMainThread, DissmissLoaderInMainThread, ReloadActivity, ShowError, ShowErrorNotEnternet));
            }

            _nestedScrollView = FindViewById<NestedScrollView>(Resource.Id.nested);
        }

        private void ReloadActivity(Shared.Models.Credit newCredit)
        {
            var intent = new Intent(this, typeof(CreditActivity));
            intent.PutExtra("idCredit", JsonConvert.SerializeObject(newCredit));
            StartActivity(intent);
        }

        protected override void SetDatasAfterLoad()
        {
            base.SetDatasAfterLoad();

            _nestedScrollView.ScrollTo(0,0);
        }
    }
}