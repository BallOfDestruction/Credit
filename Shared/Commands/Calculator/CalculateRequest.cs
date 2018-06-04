using System;
using System.Globalization;
using Shared.WebService;

namespace Credit.Commands.Calculator
{
    public class CalculateRequest
    {
        public string Duration => _duration;
        public string Amount => _amount;
        public string Percent => _percent;
        public string Type => _type;

        private string _duration;
        private string _amount;
        private string _percent;
        private string _type;

        public CalculateRequest(string duration, string amount, string percent, string type)
        {
            this._duration = duration;
            this._amount = amount;
            this._percent = percent;
            this._type = type;
        }

        public bool IsValid(Action<Error> showError)
        {
            if (string.IsNullOrEmpty(_type) || (_type != "Дифференцированный" && _type != "Аннуитет"))
            {
                showError(new Error("name", "Введите тип выплат. Доступные типы: Дифференцированный, Аннуитет"));
                return false;
            }

            if (string.IsNullOrEmpty(_percent))
            {
                showError(new Error("name", "Введите процентную ставку"));
                return false;
            }

            _percent = _percent?.Replace(",", ".");
            if (!float.TryParse(_percent, NumberStyles.AllowDecimalPoint, null, out var procent))
            {
                showError(new Error("name", "Процент ввведен в неверном формате или равен 0"));
                return false;
            }

            if (string.IsNullOrEmpty(_duration))
            {
                showError(new Error("name", "Введите длительность"));
                return false;
            }

            if (!int.TryParse(_duration, out var duration))
            {
                showError(new Error("name", "Длительность ввведена в неверном формате или равна 0"));
                return false;
            }

            if (string.IsNullOrEmpty(_amount))
            {
                showError(new Error("name", "Введите сумму"));
                return false;
            }

            if (!long.TryParse(_amount, out var anount))
            {
                showError(new Error("name", "Сумма ввведена в неверном формате или равна 0"));
                return false;
            }

            return true;
        }
    }
}