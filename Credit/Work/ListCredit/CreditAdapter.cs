using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;

namespace Credit.Work.ListCredit
{
    public class CreditAdapter : RecyclerView.Adapter
    {
        private readonly Action<Shared.Models.Credit> _open;
        private readonly List<Shared.Models.Credit> _credits;

        public CreditAdapter(List<Shared.Models.Credit> credits, Action<Shared.Models.Credit> open)
        {
            _open = open;
            _credits = credits ?? new List<Shared.Models.Credit>();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _credits[position];

            if (item == null || !(holder is CreditViewHolder viewHolder)) return;

            viewHolder.Title.Text = item.Name;
            viewHolder.Subtitle.Text = $"{item.BankName}, {item.Amount} руб., {item.DurationInMonth} мес.";
            viewHolder.Icon.SetImageResource(Resource.Mipmap.ic_payment_black_24dp);
            viewHolder.Icon.SetColorFilter(Color.Green);
            viewHolder.Main.Click += null;
            viewHolder.Main.Click += (sender, args) => _open?.Invoke(item);

            if (item.IsPay)
            {
                viewHolder.Icon.SetColorFilter(Color.Green);
            }
            else
            {
                viewHolder.Icon.SetColorFilter(Color.Red);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.credit_item, parent, false);
            var viewHolder = new CreditViewHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => _credits.Count;
    }
}