using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Newtonsoft.Json;
using Shared.Models;

namespace Credit.Work.CalculaterResult
{
    [Activity(Label = "Расчеты", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]
    public class CalculatorResultActivity : ReturnActivity
    {
        private NestedScrollView _nestedScrollView;
        protected override string SupportTitle => "Расчеты";
        protected override int LayoutResId => Resource.Layout.calculator_result;

        protected override void LoadSyncElements()
        {
            base.LoadSyncElements();

            var credit = JsonConvert.DeserializeObject<Shared.Models.Credit>(Intent.GetStringExtra("credit"));
            var payments = JsonConvert.DeserializeObject<List<PaymentModel>>(credit.ListPayment ?? "");

            var duration = FindViewById<TextView>(Resource.Id.calculate_result_duration);
            duration.Text = credit.DurationInMonth.ToString("N", CultureInfo.CurrentCulture);

            var procent = FindViewById<TextView>(Resource.Id.calculate_result_percent);
            procent.Text = credit.Procent.ToString("N", CultureInfo.CurrentCulture);

            var amount = FindViewById<TextView>(Resource.Id.calculate_result_amount);
            amount.Text = credit.Amount.ToString("N", CultureInfo.CurrentCulture);

            var type = FindViewById<TextView>(Resource.Id.calculate_result_type);
            type.Text = credit.TypeCredit;

            var amountWithPercent = FindViewById<TextView>(Resource.Id.calculate_result_amount_with_percent);
            amountWithPercent.Text = payments.Sum(w => w.Summ).ToString("N", CultureInfo.CurrentCulture);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.calculate_result_payment);
            recyclerView.SetLayoutManager(new NotCanScrollableLinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            recyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            recyclerView.SetAdapter(new CalculaterResultAdapter(payments.ToArray()));

            _nestedScrollView = FindViewById<NestedScrollView>(Resource.Id.nested);
        }

        protected override void SetDatasAfterLoad()
        {

            RunOnUiThread(() =>
            {
                _nestedScrollView.ScrollTo(0, 0);
            });
        }

        private class NotCanScrollableLinearLayoutManager : LinearLayoutManager
        {
            public override bool CanScrollVertically()
            {
                return false;
            }

            protected NotCanScrollableLinearLayoutManager(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
            {
            }

            public NotCanScrollableLinearLayoutManager(Context context) : base(context)
            {
            }

            public NotCanScrollableLinearLayoutManager(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
            {
            }

            public NotCanScrollableLinearLayoutManager(Context context, int orientation, bool reverseLayout) : base(context, orientation, reverseLayout)
            {
            }
        }
    }
}