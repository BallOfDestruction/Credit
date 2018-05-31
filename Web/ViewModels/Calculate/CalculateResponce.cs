
using Web.Models;

namespace Web.ViewModels.Calculate
{
    public class CalculateResponce
    {
        public Web.Models.Credit Credit { get; set; }

        public CalculateResponce()
        {
            
        }

        public CalculateResponce(Credit credit)
        {
            Credit = credit;
        }
    }
}