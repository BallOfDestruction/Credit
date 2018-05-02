namespace Shared.Commands.PayCredit
{
    public class PayCreditRequest
    {
        public int CreditId { get; set; }
        public int PaymentId { get; set; }

        public PayCreditRequest()
        {
            
        }

        public PayCreditRequest(int creditId, int paymentId)
        {
            CreditId = creditId;
            PaymentId = paymentId;
        }
    }
}
