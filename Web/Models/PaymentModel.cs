using System;

namespace Web.Models
{
    public class PaymentModel
    {
        public DateTime Date { get; set; }
        public float Summ { get; set; }
        public int Id { get; set; }
        public bool IsPay { get; set; }

        public float MainDebt { get; set; }
        public float ProcentDebt { get; set; }

        public PaymentModel()
        {
        }

        public PaymentModel(DateTime date, float summ, int id, bool isPay, float mainDebt, float procentDebt)
        {
            Date = date;
            Summ = summ;
            Id = id;
            IsPay = isPay;
            MainDebt = mainDebt;
            ProcentDebt = procentDebt;
        }
    }
}
