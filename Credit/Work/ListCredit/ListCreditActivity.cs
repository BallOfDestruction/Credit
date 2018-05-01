using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Credit.Base;
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

        protected override void LoadSyncElements()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.list_credit_resyclerView);
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
        }

        private void OnSuccess(GetListResponce getListResponce)
        {
            _credits = getListResponce?.Credits;
        }

        private void OpenCredit(Shared.Models.Credit credit)
        {
            //TODO start
            ShowError("Ошибка", credit.Name);
        }
    }
}