using System;
using Shared.WebService;

namespace Shared.Commands.CustomPay
{
    public class CustomPayRequest
    {
        public float Amount { get; set; }
        public int IdCredit { get; set; }

        public CustomPayRequest()
        {
            
        }

        public CustomPayRequest(float amount, int idCredit)
        {
            Amount = amount;
            IdCredit = idCredit;
        }

        public bool IsValid(Action<Error> showError)
        {
            if (Amount == 0)
            {
                showError?.Invoke(new Error("zero", "Введите сумму"));
                return false;
            }

            return true;
        }
    }
}
