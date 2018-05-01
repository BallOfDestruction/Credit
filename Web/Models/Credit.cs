using System;
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
    }
}
