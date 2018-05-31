using Shared.Database;

namespace Shared.Models
{
    public class AvialableCredit : Entity
    {
        public string BankName { get; set; }
        public float MaxAmount { get; set; }
        public float Percent { get; set; }
        public int MaxDuration { get; set; }

        public string Url { get; set; }
        public AvialableCredit()
        {
            
        }

        public AvialableCredit(string bankName, float maxAmount, float percent, int maxDuration, string url)
        {
            BankName = bankName;
            MaxAmount = maxAmount;
            Percent = percent;
            MaxDuration = maxDuration;
            Url = url;
        }
    }
}
