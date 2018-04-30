using Android.App;
using Android.Views;
using Credit.Base;

namespace Credit.ListCredit
{
    [Activity(Label = "Список кредитов", Theme = "@style/MyCustomTheme", WindowSoftInputMode = SoftInput.StateHidden)]

    public class ListCreditActivity : BurgerActivity
    {
        protected override string SupportTitle => "Список кредитов";
        protected override int LayoutResId => Resource.Layout.list_credit;
    }
}