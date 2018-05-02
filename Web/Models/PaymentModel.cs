using System;

namespace Web.Models
{
    public class PaymentModel
    {
        public DateTime Date { get; set; }
        public float Summ { get; set; }
        public int Id { get; set; }
        public bool IsPay { get; set; }

        public PaymentModel()
        {
        }

        public PaymentModel(DateTime date, float summ, int id, bool isPay)
        {
            Date = date;
            Summ = summ;
            Id = id;
            IsPay = isPay;
        }
    }
}
