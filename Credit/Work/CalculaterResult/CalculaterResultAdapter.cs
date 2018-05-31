using System.Globalization;
using Android.Support.V7.Widget;
using Android.Views;
using Shared.Models;

namespace Credit.Work.CalculaterResult
{
    public class CalculaterResultAdapter : RecyclerView.Adapter
    {
        private readonly PaymentModel[] _paymentModels;

        public CalculaterResultAdapter(PaymentModel[] paymentModels)
        {
            _paymentModels = paymentModels;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is CalculatorResultViewHolder viewHolder)
            {
                var item = _paymentModels[position];

                viewHolder.Pay.Text = item.Summ.ToString("N", CultureInfo.CurrentCulture);
                viewHolder.MainDebt.Text = item.MainDebt.ToString("N", CultureInfo.CurrentCulture);
                viewHolder.Number.Text = (position+1).ToString();
                viewHolder.Procent.Text = item.ProcentDebt.ToString("N", CultureInfo.CurrentCulture);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.culculator_result_item, parent, false);

            return new CalculatorResultViewHolder(view);
        }

        public override int ItemCount => _paymentModels.Length;
    }
}