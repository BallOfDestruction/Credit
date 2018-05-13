using System;
using System.Globalization;

namespace Shared.Commands.CreateCredit
{
    public class CreateCreditRequest
    {
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Type { get; set; }
        public float Procent { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public long Amount { get; set; }

        public CreateCreditRequest()
        {
            
        }

        public CreateCreditRequest(CreateCreditRequestModel model)
        {
            Name = model.Name;
            Bank = model.Bank;
            Type = model.Type;
            Procent = float.Parse(model.Procent);
            Duration = int.Parse(model.Duration);
            StartDate = DateTime.Parse(model.StartDate, new CultureInfo("ru"));
            Amount = long.Parse(model.Amount);
        }
    }
}
