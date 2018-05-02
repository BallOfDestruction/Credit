namespace Web.Models
{
    public class AvialableCredit : Entity<AvialableCredit>
    {
        public string BankName { get; set; }
        public float MaxAmount { get; set; }
        public float Percent { get; set; }
        public int MaxDuration { get; set; }

        public AvialableCredit()
        {
            
        }

        public AvialableCredit(string bankName, float maxAmount, float percent, int maxDuration)
        {
            BankName = bankName;
            MaxAmount = maxAmount;
            Percent = percent;
            MaxDuration = maxDuration;
        }
    }
}
