using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Commands.PayCredit;
using Shared.Database;
using Shared.Delegates;
using Shared.Models;
using Shared.WebService;

namespace Credit.Work.Credit
{
    public class PaymentAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private readonly int _creditId;
        private readonly Action _showLoader;
        private readonly Action _hideLoader;
        private readonly Action<Shared.Models.Credit> _reloadActivity;
        private readonly Action<Error> _showError;
        private readonly Action _showErrorNotEnternet;
        private readonly List<PaymentModel> _payment;

        public PaymentAdapter(Context context, List<PaymentModel> payment, int creditId, Action showLoader, Action hideLoader, Action<Shared.Models.Credit> reloadActivity, Action<Error> showError, Action showErrorNotEnternet)
        {
            _context = context;
            _creditId = creditId;
            _showLoader = showLoader;
            _hideLoader = hideLoader;
            _reloadActivity = reloadActivity;
            _showError = showError;
            _showErrorNotEnternet = showErrorNotEnternet;
            _payment = payment.OrderBy(w => w.Id).ThenBy(w => w.Date).ToList();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _payment[position];

            if (item == null || !(holder is PaymentViewHolder viewHolder)) return;

            viewHolder.Amount.Text = item.Summ.ToString();
            viewHolder.Date.Text = item.Date.ToString("d", new CultureInfo("ru"));

            if (item.IsPay)
            {
                viewHolder.Icon.SetColorFilter(Color.Green);
                viewHolder.Apply.Visibility = ViewStates.Gone;
                viewHolder.Apply.Click += null;
                viewHolder.PaymentText.Visibility = ViewStates.Visible;
            }
            else
            {
                viewHolder.Icon.SetColorFilter(Color.Gray);
                viewHolder.Apply.Click += null;
                viewHolder.Apply.Click += (sender, args) => ApplyOnClick(item.Id);
                viewHolder.Apply.Visibility = ViewStates.Visible;
                viewHolder.PaymentText.Visibility = ViewStates.Gone;
            }
        }

        private void ApplyOnClick(int id)
        {
            var builder = new AlertDialog.Builder(_context);
            builder.SetTitle("Вы точно хотите выполнить оплату?");
            builder.SetPositiveButton("ОК", (o, args) =>
            {
                ThreadPool.QueueUserWorkItem(w =>
                {
                    _showLoader?.Invoke();
                    var model = new PayCreditRequest(_creditId, id);
                    var commandDelegate = new CommandDelegate<PayCreditResponce>(responce => _reloadActivity?.Invoke(responce.Credit), _showError, _showErrorNotEnternet);
                    var command = new PayCreditCommand(LocalDb.Instance, commandDelegate);
                    command.Execute(model);
                    _hideLoader?.Invoke();
                });
            });
            builder.SetNegativeButton("Нет", (o, args) => { });
            builder.Create().Show();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.payment_item, parent, false);
            var viewHolder = new PaymentViewHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => _payment.Count;
    }
}