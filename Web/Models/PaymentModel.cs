using System;

namespace Web.Models
{
    public class PaymentModel
    {
        public DateTime Date { get; set; }
        public float Summ { get; set; }
        public int Position { get; set; }
        public bool IsPay { get; set; }
    }
}
