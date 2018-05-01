using System;
using Shared.Database;

namespace Shared.Models
{
    public class Credit : Entity
    {
        public string Name { get; set; }

        public string BankName { get; set; }

        public DateTime StartCredit { get; set; }

        public float Procent { get; set; }

        public float Amount { get; set; }

        public int DurationInMonth { get; set; }

        //Дифференцированный, Аннуитет
        public string TypeCredit { get; set; }
        
        //Json выплат
        public string ListPayment { get; set; }
    }
}
