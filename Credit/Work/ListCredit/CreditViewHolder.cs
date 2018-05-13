using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Credit.Work.ListCredit
{
    public class CreditViewHolder : RecyclerView.ViewHolder
    {
        public LinearLayout Main { get; set; }
        public ImageView Icon { get; set; }
        public TextView Title { get; set; }
        public TextView Subtitle { get; set; }
        public Shared.Models.Credit Credit { get; set; }

        public CreditViewHolder(View itemView) : base(itemView)
        {
            Main = itemView.FindViewById<LinearLayout>(Resource.Id.credit_item_main);
            Icon = itemView.FindViewById<ImageView>(Resource.Id.credit_item_icon);
            Title = itemView.FindViewById<TextView>(Resource.Id.credit_item_title);
            Subtitle = itemView.FindViewById<TextView>(Resource.Id.credit_item_subtitle);
        }
    }
}