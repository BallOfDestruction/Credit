using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Web.Controllers.cms;

namespace Web.Models
{
    [Access(false)]
    public class Credit : Entity<Credit>
    {
        [DisplayName("Название кредита")]
        public string Name { get; set; }

        [DisplayName("Имя банка")]
        public string BankName { get; set; }

        [DisplayName("Дата взятия")]
        public DateTime StartCredit { get; set; }

        [DisplayName("Процентная ставка")]
        public float Procent { get; set; }

        [DisplayName("Сумма")]
        public float Amount { get; set; }

        [DisplayName("Длительность в месяцах")]
        public int DurationInMonth { get; set; }

        //Дифференцированный, Аннуитет
        [DisplayName("Тип кредита")]
        public string TypeCredit { get; set; }

        //Json выплат
        [Show(false, false, false, false)]
        public string ListPayment { get; set; }

        [DisplayName("Длительность в месяцах")]
        public bool IsPay { get; set; }

        [Show(false, false, false, false)]
        [JsonIgnore]
        public int UserId { get; set; }

        [DisplayName("Пользователь")]
        [ShowTitle]
        [JsonIgnore]
        public User User { get; set; }

        public override string GetValueFromNameProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "IsPay":
                    return IsPay ? "Оплачено" : "Неоплачено";
                default:
                    return null;
            }
        }

        public void InitPayment()
        {
            if (TypeCredit == "Дифференцированный")
                InitDiff();
            else
                InitAut();
        }

        private void InitAut()
        {
            var p = (Procent / 12f) / 100;
            var d = p / (Math.Pow((1 + p), DurationInMonth) - 1);
            var summPay =(float) (Amount * (p + d));
            var listPay = new List<PaymentModel>();
            for (var i = 0; i < DurationInMonth; i++)
            {
                listPay.Add(new PaymentModel(StartCredit.AddMonths(i), summPay, i, false));
            }
            ListPayment = JsonConvert.SerializeObject(listPay);
        }

        private void InitDiff()
        {
            var b = Amount / DurationInMonth;
            var listPay = new List<PaymentModel>();
            for (var i = 0; i < DurationInMonth; i++)
            {
                var sn = Amount - (i * b);
                var p = b + sn * (Procent / 12f / 100);
                listPay.Add(new PaymentModel(StartCredit.AddMonths(i), p, i, false));
            }
            ListPayment = JsonConvert.SerializeObject(listPay);
        }
    }
}
