using System;
using Newtonsoft.Json;
using Shared.Database;

namespace Shared.Models
{
    public class Credit : Entity
    {
        [JsonProperty("Id")]
        public int ServerId { get; set; }

        public string Name { get; set; }

        public string BankName { get; set; }

        public DateTime StartCredit { get; set; }

        public float Procent { get; set; }
        public float Rest { get; set; }

        public long Amount { get; set; }

        public int DurationInMonth { get; set; }

        //Дифференцированный, Аннуитет
        public string TypeCredit { get; set; }

        public bool IsPay { get; set; }
        //Json выплат
        public string ListPayment { get; set; }
    }
}
