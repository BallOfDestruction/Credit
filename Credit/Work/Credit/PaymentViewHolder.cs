using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Credit.Work.Credit
{
    class PaymentViewHolder : RecyclerView.ViewHolder
    {
        public Button Apply { get; set; }
        public TextView Date { get; set; }
        public TextView Amount { get; set; }
        public ImageView Icon { get; set; }
        public TextView PaymentText { get; set; }

        public PaymentViewHolder(View itemView) : base(itemView)
        {
            Apply = itemView.FindViewById<Button>(Resource.Id.payment_pay);
            Date = itemView.FindViewById<TextView>(Resource.Id.payment_date);
            Amount = itemView.FindViewById<TextView>(Resource.Id.payment_amount);
            Icon = itemView.FindViewById<ImageView>(Resource.Id.payment_item_icon);
            PaymentText = itemView.FindViewById<TextView>(Resource.Id.payment_pay_text);
        }
    }
}