using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Credit.Base;
using Shared.Commands.AvialableCredit;
using Shared.Database;
using Shared.Delegates;
using Shared.Models;

namespace Credit.Work.АvailableCredit
{
    [Activity(Label = "Доступные кредиты", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]

    public class AvialableCreditActivity : BurgerActivity
    {
        protected override bool IsShowLoader => true;
        protected override string SupportTitle => "Доступные кредиты";
        protected override int LayoutResId => Resource.Layout.avialable_list_credit;

        private RecyclerView _avialableRecyclerView;
        private List<AvialableCredit> _avialableCredits;

        protected override void LoadSyncElements()
        {
            _avialableRecyclerView = FindViewById<RecyclerView>(Resource.Id.avialable_list_credit_resyclerView);
            _avialableRecyclerView.AddItemDecoration(new DividerItemDecoration(this, DividerItemDecoration.Vertical));
            _avialableRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected override void LoadAsyncElements()
        {
            var commandDelegate = new CommandDelegate<AvialableCreditResponce>(OnSuccess, ShowError, ShowErrorNotEnternet);
            var command = new AvialableCreditCommand(LocalDb.Instance, commandDelegate);
            command.Execute(new AvialableCreditRequest());
        }

        private void OnSuccess(AvialableCreditResponce avialableCreditResponce)
        {
            _avialableCredits = avialableCreditResponce.AvialableCredits;
        }

        protected override void SetDatasAfterLoad()
        {
            if (_avialableCredits == null) return;

            _avialableRecyclerView.SetAdapter(new AvialableCreditAdapter(_avialableCredits));
        }
    }
}