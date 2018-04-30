using Android.Views;
using Android.Widget;

namespace Credit.Base
{
    /// <summary>
    /// Базовое активити
    /// </summary>
    public abstract class ReturnActivity : BaseActivity
    {
        //Левая верхняя кнопка(бургер или назад)
        protected override int? LeftButtonId => Resource.Mipmap.ic_action_arrow_back;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    Finish();
                    return base.OnOptionsItemSelected(item);
                default:
                    Toast.MakeText(this, "Action selected: " + item.TitleFormatted, ToastLength.Short).Show();
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}