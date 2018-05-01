using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Models;

namespace Credit.Work.Credit
{
    public class PaymentAdapter : RecyclerView.Adapter
    {
        private readonly List<PaymentModel> _payment;

        public PaymentAdapter(List<PaymentModel> payment)
        {
            _payment = payment.OrderBy(w => w.Position).ToList();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _payment[position];

            if (item == null || !(holder is PaymentViewHolder viewHolder)) return;

            viewHolder.Amount.Text = item.Summ.ToString();
            viewHolder.Date.Text = item.Date.ToString("d", new CultureInfo("ru"));

            if (item.IsPay)
            {
                viewHolder.Icon.SetColorFilter(Color.Gray);
                viewHolder.Apply.Visibility = ViewStates.Gone;
                viewHolder.Apply.Click += null;
                viewHolder.PaymentText.Visibility = ViewStates.Visible;
            }
            else
            {
                viewHolder.Icon.SetColorFilter(Color.Green);
                viewHolder.Apply.Click +=null;
                viewHolder.Apply.Click += ApplyOnClick;
                viewHolder.Apply.Visibility = ViewStates.Visible;
                viewHolder.PaymentText.Visibility = ViewStates.Gone;
            }
        }

        private void ApplyOnClick(object sender, EventArgs eventArgs)
        {
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