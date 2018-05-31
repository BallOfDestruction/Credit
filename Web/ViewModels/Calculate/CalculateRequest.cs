using System;

namespace Web.ViewModels.Calculate
{
    public class CalculateRequest
    {
        public int Duration { get; set; }
        public long Amount { get; set; }
        public float Percent { get; set; }
        public string Type { get; set; }

        public CalculateRequest()
        {
            
        }

        public CalculateRequest(int duration, long amount, float percent, string type)
        {
            Duration = duration;
            Amount = amount;
            Percent = percent;
            Type = type;
        }
    }
}