using System;

namespace Shared.Models
{
    public class PaymentModel
    {
        public DateTime Date { get; set; }
        public float Summ { get; set; }
        public int Position { get; set; }
        public bool IsPay { get; set; }

        public PaymentModel()
        {
            
        }

        public PaymentModel(DateTime date, float summ, int position, bool isPay)
        {
            Date = date;
            Summ = summ;
            Position = position;
            IsPay = isPay;
        }
    }
}
