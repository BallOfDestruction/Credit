using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Credit.Work.Login;
using Shared.Settings;
using Shared.WebService;

namespace Credit.Base
{
    /// <summary>
    /// Базовое активити
    /// </summary>
    public abstract class BaseActivity : AccessActivity
    {
        public bool IsDidFirstLoad { get; private set; } = false;
        protected virtual bool IsAutomaticReload => true;

        protected Toast Toast;
        protected ProgressDialog ProgressDialog;

        /// <summary>
        /// Title верхней строчки
        /// </summary>
        protected abstract string SupportTitle { get; }

        /// <summary>
        /// Id слоя
        /// </summary>
        protected abstract int LayoutResId { get; }

        /// <summary>
        /// Левая верхняя кнопка(бургер или назад)
        /// </summary>
        protected abstract int? LeftButtonId { get; }

        protected virtual bool IsShowLoader => false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (LayoutResId != -1)
                SetContentView(LayoutResId);

            if (SupportActionBar != null && typeof(LoginActivity) != this.GetType())
            {
                SupportActionBar.Title = SupportTitle;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                if (LeftButtonId.HasValue)
                {
                    SupportActionBar.SetHomeAsUpIndicator(LeftButtonId.Value);
                }
            }

            LoadSyncElements();

            if (IsAutomaticReload || (!IsAutomaticReload && savedInstanceState == null))
                LoadData();
        }

        /// <summary>
        /// При вызове этого метода вы обязаны вызвать метод DismissLoaderInMainThread!!!
        /// IsShowLoader не влияент на этот метод
        /// </summary>
        public void ShowLoaderInMainThread()
        {
            RunOnUiThread(() =>
            {
                ProgressDialog = ProgressDialog.Show(this, "Пожалуйста, подождите...", "Загрузка данных", true);
            });
        }

        public void DissmissLoaderInMainThread()
        {
            RunOnUiThread(() =>
            {
                ProgressDialog?.Hide();
                ProgressDialog?.Dismiss();
            });
        }
        /// <summary>
        /// Использовать для перезагрузки данных.
        /// При переопределении LoadAsyncElements и SetDatasAfterLoad.
        /// </summary>
        public void LoadData()
        {
            LoadData(IsShowLoader);
        }

        public void LoadData(bool isLoad)
        {
            LoadData(isLoad, SetDatasAfterLoad);
        }

        public void LoadData(bool isLoad, Action setDatasAfterLoad)
        {
            if (isLoad)
                ShowLoaderInMainThread();

            var thread = new Thread(() =>
            {
                LoadAsyncElements();
                RunOnUiThread(() =>
                {
                    setDatasAfterLoad.Invoke();

                    if (isLoad)
                    {
                        var fakeThread = new Thread(() =>
                        {
                            DissmissLoaderInMainThread();
                        });
                        fakeThread.Start();
                    }

                    IsDidFirstLoad = true;
                });
            });
            thread.Start();
        }

        /// <summary>
        /// Переопределять для установления значений после загрузки.
        /// Выполняет в главном потоке.
        /// Выполнять только быстрые операции.
        /// </summary>
        protected virtual void SetDatasAfterLoad() { }

        /// <summary>
        /// Асинхронная загрузка.
        /// Переопределять для выполнения долгих операций по типу загрузки из интернета.
        /// Операции с активити не будут выполнятся в данном методе.
        /// </summary>
        protected virtual void LoadAsyncElements() { }

        /// <summary>
        /// Синхронная загрузка элементов.
        /// Выполняется в OnCreate.
        /// Делать только быстрые операции.
        /// </summary>
        protected virtual void LoadSyncElements() { }

        public void ShowError(string title, string text)
        {
            RunOnUiThread(() =>
            {
                Toast?.Cancel();
                Toast = Toast.MakeText(this, text, ToastLength.Long);
                Toast.Show();
            });
        }

        public void ShowError(Error error)
        {
            ShowError(WSString.Error, error.ErrorDescription);
        }

        public void ShowErrorNotEnternet()
        {
            ShowError(WSString.Error, WSString.ErrorNotInternet);
        }


        protected void HideKeyboard()
        {
            try
            {
                var imm = (InputMethodManager) this.GetSystemService(Activity.InputMethodService);

                var view = CurrentFocus ?? new View(this);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
            catch
            {
                
            }
        }

        public override void OnBackPressed()
        {
            HideKeyboard();
            base.OnBackPressed();
        }
    }
}