using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels.Avialable
{
    public class AvialableCreditResponce
    {
        public List<Models.AvialableCredit> AvialableCredits { get; set; }

        public AvialableCreditResponce()
        {
            
        }

        public AvialableCreditResponce(List<AvialableCredit> avialableCredits)
        {
            AvialableCredits = avialableCredits;
        }
    }
}
