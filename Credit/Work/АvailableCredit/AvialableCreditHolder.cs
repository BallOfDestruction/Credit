using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Credit.Work.АvailableCredit
{
    public class AvialableCreditHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; set; }
        public TextView SubTitle { get; set; }
        public TextView SubSubTitle { get; set; }

        public AvialableCreditHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.avialable_credit_item_title);
            SubTitle = itemView.FindViewById<TextView>(Resource.Id.avialable_credit_item_subtitle);
            SubSubTitle = itemView.FindViewById<TextView>(Resource.Id.avialable_credit_item_subsubtitle);
        }
    }
}