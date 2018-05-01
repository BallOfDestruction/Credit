using System.Collections.Generic;

namespace Shared.Commands.ListCredit
{
    public class GetListResponce
    {
        public List<Models.Credit> Credits { get; set; }

        public GetListResponce(List<Models.Credit> credits)
        {
            Credits = credits;
        }

        public GetListResponce()
        {
            
        }
    }
}
