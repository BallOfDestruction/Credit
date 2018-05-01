using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Credit.Work.Login;

namespace Credit.Base
{
    /// <summary>
    /// Базовое активити
    /// </summary>
    public abstract class BurgerActivity : BaseActivity
    {
        protected sealed override int? LeftButtonId => Resource.Mipmap.burger;
        private DrawerLayout _mDrawerLayout;

        protected override void LoadSyncElements()
        {
            //Выпадающее меню слева
            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var mLeftDrawer = FindViewById<NavigationView>(Resource.Id.left_drawer);

            if (mLeftDrawer != null)
            {
                SetUpDrawerContent(mLeftDrawer);
                //var leftMenu = mLeftDrawer.GetHeaderView(0);
                //leftMenu.FindViewById<TextView>(Resource.Id.nameCurrentUser).Text = currentUser.Name;
            }
        }

        //Навигация по клику
        private void SetUpDrawerContent(NavigationView mLeftDrawer)
        {
            mLeftDrawer.NavigationItemSelected += (sender, e) =>
            {
                Intent intent;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.list_credit_nav:
                        intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                        break;
                }
                _mDrawerLayout.CloseDrawers();
            };
        }
        //Функция при выборе пункта меню
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //Обрабатывает клик по бургеру, и выводит навигацию
                    _mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}