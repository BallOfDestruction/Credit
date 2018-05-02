using System;
using System.Globalization;
using Shared.WebService;

namespace Shared.Commands.CreateCredit
{
    public class CreateCreditRequestModel
    {
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Type { get; set; }
        public string Procent { get; set; }
        public string Duration { get; set; }
        public string StartDate { get; set; }
        public string Amount { get; set; }

        public CreateCreditRequestModel()
        {
            
        }

        public CreateCreditRequestModel(string name, string bank, string type, string procent, string duration, string startDate, string amount)
        {
            Name = name;
            Bank = bank;
            Type = type;
            Procent = procent;
            Duration = duration;
            StartDate = startDate;
            Amount = amount;
        }

        public bool IsValid(Action<Error> showError)
        {
            if (string.IsNullOrEmpty(Name))
            {
                showError(new Error("name","Введите наименование кредита"));
                return false;
            }

            if (string.IsNullOrEmpty(Bank))
            {
                showError(new Error("name", "Введите наименование банка"));
                return false;
            }

            if (string.IsNullOrEmpty(Type) || (Type != "Дифференцированный" && Type != "Аннуитет"))
            {
                showError(new Error("name", "Введите тип выплат. Доступные типы: Дифференцированный, Аннуитет"));
                return false;
            }

            if (string.IsNullOrEmpty(Procent))
            {
                showError(new Error("name", "Введите процентную ставку"));
                return false;
            }

            if (!float.TryParse(Procent, out var procent))
            {
                showError(new Error("name", "Процент ввведен в неверном формате или равен 0"));
                return false;
            }

            if (string.IsNullOrEmpty(Duration))
            {
                showError(new Error("name", "Введите длительность"));
                return false;
            }

            if (!int.TryParse(Duration, out var duration))
            {
                showError(new Error("name", "Длительность ввведена в неверном формате или равна 0"));
                return false;
            }

            if (string.IsNullOrEmpty(Amount))
            {
                showError(new Error("name", "Введите сумму"));
                return false;
            }

            if (!float.TryParse(Amount, out var anount))
            {
                showError(new Error("name", "Сумма ввведена в неверном формате или равна 0"));
                return false;
            }

            if (string.IsNullOrEmpty(StartDate))
            {
                showError(new Error("name", "Введите начало выплат"));
                return false;
            }

            if (!DateTime.TryParse(StartDate, new CultureInfo("ru"), DateTimeStyles.None, out var starrtdate))
            {
                showError(new Error("name", "Сумма ввведена в неверном формате или равна 0"));
                return false;
            }

            return true;
        }
    }
}
