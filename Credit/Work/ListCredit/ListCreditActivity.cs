using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Credit.Base;
using Credit.Work.CreateCredit;
using Credit.Work.Credit;
using Newtonsoft.Json;
using Shared.Commands.ListCredit;
using Shared.Database;
using Shared.Delegates;

namespace Credit.Work.ListCredit
{
    [Activity(Label = "Список кредитов", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]

    public class ListCreditActivity : BurgerActivity
    {
        protected override string SupportTitle => "Список кредитов";
        protected override int LayoutResId => Resource.Layout.list_credit;
        protected override bool IsShowLoader => true;
        private List<Shared.Models.Credit> _credits;

        private RecyclerView _recyclerView;
        private SwipeRefreshLayout _refresher;
        private FloatingActionButton _createCreditButton;

        protected override void LoadSyncElements()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.list_credit_resyclerView);
            var layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(layoutManager);
            _recyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));

            _refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.list_credit_refresher);
            _refresher.Refresh += RefresherOnRefresh;

            _createCreditButton = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _createCreditButton.Click += CreateCreditButtonOnClick;
        }

        private void CreateCreditButtonOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(CreateCreditActivity));
        }

        private void RefresherOnRefresh(object sender, EventArgs eventArgs)
        {
            LoadData();
        }

        protected override void LoadAsyncElements()
        {
            var commandDelegate = new CommandDelegate<GetListResponce>(OnSuccess, ShowError, ShowErrorNotEnternet);
            var command = new GetListCommand(LocalDb.Instance, commandDelegate);
            command.Execute(null);
        }

        protected override void SetDatasAfterLoad()
        {
            if (_credits == null) return;

            _recyclerView.SetAdapter(new CreditAdapter(_credits, OpenCredit));
            _refresher.Refreshing = false;
        }

        private void OnSuccess(GetListResponce getListResponce)
        {
            _credits = getListResponce?.Credits;
        }

        private void OpenCredit(Shared.Models.Credit credit)
        {
            var intent = new Intent(this, typeof(CreditActivity));
            intent.PutExtra("idCredit", JsonConvert.SerializeObject(credit));
            StartActivity(intent);
        }
    }
}