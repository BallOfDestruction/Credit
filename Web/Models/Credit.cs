using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [DisplayName("Остаток на счету")]
        public float Rest { get; set; }

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

            var localAmount = Amount;
            for (var i = 0; i < DurationInMonth; i++)
            {
                var percentLocal = localAmount * p;
                var mainPay = summPay - percentLocal;
                localAmount -= mainPay;
                listPay.Add(new PaymentModel(StartCredit.AddMonths(i), summPay, i, false, mainPay, percentLocal));
            }
            ListPayment = JsonConvert.SerializeObject(listPay);
        }

        private void InitDiff()
        {
            var mainPay = Amount / DurationInMonth;
            var listPay = new List<PaymentModel>();
            for (var i = 0; i < DurationInMonth; i++)
            {
                var sn = Amount - (i * mainPay);
                var percentPay = sn * (Procent / 12f / 100);
                var p = mainPay + percentPay;
                listPay.Add(new PaymentModel(StartCredit.AddMonths(i), p, i, false, mainPay, percentPay));
            }
            ListPayment = JsonConvert.SerializeObject(listPay);
        }

        public void Recalculate()
        {
            if (Rest == 0) return;

            if (TypeCredit == "Дифференцированный")
                RecalculateDiff();
            else
                RecalculateAut();
        }

        private void RecalculateAut()
        {
            var payment = JsonConvert.DeserializeObject<List<PaymentModel>>(ListPayment);
            var notPayment = payment.Where(w => !w.IsPay).ToList();
            var pay = payment.Where(w => w.IsPay).ToList();
            var newSumm = Amount - pay.Sum(w => w.MainDebt) - Rest;
            var restMonth = notPayment.Count;
            payment.Insert(pay.Count(), new PaymentModel(DateTime.Now, Rest, pay.LastOrDefault()?.Id ?? -1, true, Rest, 0));
            Rest = 0;
            //Новый расчет
            var p = (Procent / 12f) / 100;
            var d = p / (Math.Pow((1 + p), restMonth) - 1);
            var summPay = (float)(newSumm * (p + d));
            for (var i = 0; i < restMonth; i++)
            {
                notPayment[i].Summ = summPay;
            }

            ListPayment = JsonConvert.SerializeObject(payment);
        }

        private void RecalculateDiff()
        {
            var payment = JsonConvert.DeserializeObject<List<PaymentModel>>(ListPayment);

            var notPayment = payment.Where(w => !w.IsPay).ToList();
            var pay = payment.Where(w => w.IsPay).ToList();
            var newSumm = Amount - pay.Sum(w => w.MainDebt) - Rest;
            var restMonth = notPayment.Count;
            var lastPayDate = notPayment.FirstOrDefault()?.Date ?? DateTime.Now;
            pay.Add(new PaymentModel(DateTime.Now, Rest, pay.LastOrDefault()?.Id ?? -1, true, Rest, 0));
            Rest = 0;

            var mainPay = Amount / DurationInMonth;
            for (var i = 0; i < restMonth; i++)
            {
                var sn = newSumm - (i * mainPay);
                var percentPay = sn * (Procent / 12f / 100);
                var p = mainPay + percentPay;
                pay.Add(new PaymentModel(lastPayDate.AddMonths(i), p, pay.Count, false, mainPay, percentPay));
            }

            ListPayment = JsonConvert.SerializeObject(pay);
        }
    }
}
