using System.Collections.Generic;
using Android.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Credit.Base;
using Credit.Work.Credit;
using Credit.Work.АvailableCredit;
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
        private List<AvialableCredit> _avialableCredit;
        private TextView _title;
        private TextView _subTitle;
        private RecyclerView _list;
        private NestedScrollView _nestedScrollView;

        protected override void LoadSyncElements()
        {
            base.LoadSyncElements();

            _credit = JsonConvert.DeserializeObject<Shared.Models.Credit>(Intent.GetStringExtra("credit"));
            _avialableCredit = JsonConvert.DeserializeObject<List<Shared.Models.AvialableCredit>>(Intent.GetStringExtra("AvialableCredit"));

            _title = FindViewById<TextView>(Resource.Id.compare_credit_title);
            _title.Text = _credit.BankName;

            _subTitle = FindViewById<TextView>(Resource.Id.compare_credit_subtitle);
            _subTitle.Text = $"{_credit.Procent}%, {_credit.Amount} руб., на {_credit.DurationInMonth} мес.";

            _list = FindViewById<RecyclerView>(Resource.Id.compare_list);
            _list.SetLayoutManager(new NotCanScrollGridLayoutManager(this, 1));
            _list.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            _list.SetAdapter(new AvialableCreditAdapter(_avialableCredit));
            _list.NestedScrollingEnabled = false;

            _nestedScrollView = FindViewById<NestedScrollView>(Resource.Id.nested);
        }

        protected override void SetDatasAfterLoad()
        {
            base.SetDatasAfterLoad();

            _nestedScrollView.ScrollTo(0, 0);
        }
    }
}