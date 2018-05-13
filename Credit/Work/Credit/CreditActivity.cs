using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Credit.Work.Compare;
using Newtonsoft.Json;
using Shared.Commands.AvialableCredit;
using Shared.Commands.CustomPay;
using Shared.Commands.PayNow;
using Shared.Commands.Recalculating;
using Shared.Database;
using Shared.Delegates;
using Shared.Models;
using Shared.WebService;

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
        private Button _findBetter;
        private TextView _rest;
        private EditText _inputPay;
        private Button _buttonPay;

        private Button _payNoewButton;
        private Button _recalculationButton;

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
            _percent.Text = _credit.Procent.ToString(new CultureInfo("ru"));

            _amount = FindViewById<TextView>(Resource.Id.credit_amount);
            _amount.Text = _credit.Amount.ToString("N", new CultureInfo("ru"));

            _paymentView = FindViewById<RecyclerView>(Resource.Id.credit_payment);
            _paymentView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            var layoutManager = new NotCanScrollGridLayoutManager(this, 1);
            _paymentView.SetLayoutManager(layoutManager);
            _paymentView.NestedScrollingEnabled = false;

            if (_payments?.Any() == true)
            {
                _paymentView.SetAdapter(new PaymentAdapter(this, _payments, _credit.ServerId, ShowLoaderInMainThread, DissmissLoaderInMainThread, ReloadActivity, ShowError, ShowErrorNotEnternet));
            }

            _nestedScrollView = FindViewById<NestedScrollView>(Resource.Id.nested);

            _findBetter = FindViewById<Button>(Resource.Id.credit_find_better);
            _findBetter.Click += FindBetterOnClick;

            _rest = FindViewById<TextView>(Resource.Id.credit_rest);
            _rest.Text = _credit.Rest.ToString(new CultureInfo("ru"));

            _inputPay = FindViewById<EditText>(Resource.Id.credit_input_custom_pay);

            _buttonPay = FindViewById<Button>(Resource.Id.credit_button_custom_pay);
            _buttonPay.Click += ButtonPayOnClick;

            _payNoewButton = FindViewById<Button>(Resource.Id.credit_payNow);
            _payNoewButton.Click += PayNowOnClick;

            _recalculationButton = FindViewById<Button>(Resource.Id.credit_recalculation);
            _recalculationButton.Click += RecalculateOnClick;

            if (_credit.IsPay)
            {
                _recalculationButton.Visibility = ViewStates.Gone;
                _payNoewButton.Visibility = ViewStates.Gone;
                _buttonPay.Visibility = ViewStates.Gone;
                _inputPay.Visibility = ViewStates.Gone;
            }
        }

        private void RecalculateOnClick(object sender, EventArgs e)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle("Вы точно хотите выполнить перерасчет?");
            builder.SetPositiveButton("ОК", (o, args) =>
            {
                ThreadPool.QueueUserWorkItem(w =>
                {
                    ShowLoaderInMainThread();

                    var commandDelegate =
                        new CommandDelegate<RecalculatingResponce>(OnSuccessRecalculating, ShowError,
                            ShowErrorNotEnternet);
                    var command = new RecalculatingCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(new RecalculatingRequest() {IdCredit = _credit.ServerId});

                    DissmissLoaderInMainThread();
                });
            });
            builder.SetNegativeButton("Нет", (o, args) => {});
            builder.Create().Show();
        }

        private void PayNowOnClick(object sender, EventArgs e)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle("Вы точно хотите списать с остатка и оплатить?");
            builder.SetPositiveButton("ОК", (o, args) =>
            {
                ThreadPool.QueueUserWorkItem(w =>
                {
                    ShowLoaderInMainThread();

                    var commandDelegate = new CommandDelegate<PayNowResponce>(OnSuccessPayNow, ShowError, ShowErrorNotEnternet);
                    var command = new PayNowCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(new PayNowRequest() { IdCredit = _credit.ServerId });

                    DissmissLoaderInMainThread();
                });
            });
            builder.SetNegativeButton("Нет", (o, args) => { });
            builder.Create().Show();
        }

        private void ButtonPayOnClick(object sender, EventArgs eventArgs)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle("Вы точно хотите внести на счет?");
            builder.SetPositiveButton("ОК", (o, args) =>
            {
                float.TryParse(_inputPay.Text, out var amount);

                var model = new CustomPayRequest(amount, _credit.ServerId);

                if (!model.IsValid(ShowError)) return;

                ThreadPool.QueueUserWorkItem(w =>
                {
                    ShowLoaderInMainThread();

                    var commandDelegate = new CommandDelegate<CustomPayResponce>(OnSuccessCustomPay, ShowError, ShowErrorNotEnternet);
                    var command = new CustomPayCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(model);

                    DissmissLoaderInMainThread();
                });
            });
            builder.SetNegativeButton("Нет", (o, args) => { });
            builder.Create().Show();
        }

        private void OnSuccessPayNow(PayNowResponce model)
        {
            RunOnUiThread(() =>
            {
                ReloadActivity(model.Credit);
            });
        }

        private void OnSuccessRecalculating(RecalculatingResponce model)
        {
            RunOnUiThread(() =>
            {
                ReloadActivity(model.Credit);
            });
        }

        private void OnSuccessCustomPay(CustomPayResponce model)
        {
            RunOnUiThread(() =>
            {
                ReloadActivity(model.Credit);
            });
        }

        private void FindBetterOnClick(object sender, EventArgs eventArgs)
        {
            ThreadPool.QueueUserWorkItem(w =>
            {
                ShowLoaderInMainThread();

                var commandDelegate = new CommandDelegate<AvialableCreditResponce>(OnSuccess, ShowError, ShowErrorNotEnternet);
                var command = new AvialableCreditCommand(LocalDb.Instance, commandDelegate);
                command.Execute(new AvialableCreditRequest());

                DissmissLoaderInMainThread();
            });
        }

        private void OnSuccess(AvialableCreditResponce avialableCreditResponce)
        {
            var better = avialableCreditResponce.AvialableCredits
                .Where(w => w.MaxAmount >= _credit.Amount && w.MaxDuration >= _credit.DurationInMonth && w.Percent < _credit.Procent)
                .OrderBy(w => w.Percent).ToList();

            if (better.Any() != true)
            {
                RunOnUiThread(() =>
                {
                    ShowError(new Error("code", "Более лучшего кредита не найдено"));
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    var intent = new Intent(this, typeof(CompareActivity));
                    intent.PutExtra("credit", JsonConvert.SerializeObject(_credit));
                    intent.PutExtra("AvialableCredit", JsonConvert.SerializeObject(better));
                    StartActivity(intent);
                });
            }
        }

        private void ReloadActivity(Shared.Models.Credit newCredit)
        {
            var intent = new Intent(this, typeof(CreditActivity));
            intent.PutExtra("idCredit", JsonConvert.SerializeObject(newCredit));
            StartActivity(intent);
            Finish();
        }

        protected override void SetDatasAfterLoad()
        {
            base.SetDatasAfterLoad();

            _nestedScrollView.ScrollTo(0,0);
        }
    }
}