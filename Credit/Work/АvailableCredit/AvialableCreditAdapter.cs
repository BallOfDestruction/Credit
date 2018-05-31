using System.Collections.Generic;
using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Models;

namespace Credit.Work.АvailableCredit
{
    public class AvialableCreditAdapter : RecyclerView.Adapter
    {
        private readonly List<AvialableCredit> _avialableCredits;

        public AvialableCreditAdapter(List<AvialableCredit> avialableCredits)
        {
            _avialableCredits = avialableCredits;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _avialableCredits[position];

            if (holder is AvialableCreditHolder viewHolder)
            {
                viewHolder.Title.Text = item.BankName;
                viewHolder.SubTitle.Text = $"{item.Percent}%, до {item.MaxAmount.ToString("N", new CultureInfo("ru"))} руб., до {item.MaxDuration} мес.";
                viewHolder.SubSubTitle.Text = item.Url;

                viewHolder.SubSubTitle.Visibility = string.IsNullOrEmpty(item.Url) ? ViewStates.Gone : ViewStates.Visible;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.avialable_credit_item, parent, false);
            var viewHolder = new AvialableCreditHolder(itemView);
            return viewHolder;
        }

        public override int ItemCount => _avialableCredits.Count;
    }
}