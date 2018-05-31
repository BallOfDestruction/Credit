using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Credit.Work.CalculaterResult
{
    public class CalculatorResultViewHolder : RecyclerView.ViewHolder
    {
        public TextView Number { get; set; }
        public TextView MainDebt { get; set; }
        public TextView Procent { get; set; }
        public TextView Pay { get; set; }

        public CalculatorResultViewHolder(View itemView) : base(itemView)
        {
            Number = itemView.FindViewById<TextView>(Resource.Id.calculator_result_number);
            MainDebt = itemView.FindViewById<TextView>(Resource.Id.calculator_result_main_debt);
            Procent = itemView.FindViewById<TextView>(Resource.Id.calculator_result_main_procent);
            Pay = itemView.FindViewById<TextView>(Resource.Id.calculator_result_main_month_pay);
        }
    }
}