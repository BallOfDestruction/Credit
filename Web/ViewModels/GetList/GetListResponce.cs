using System.Collections.Generic;

namespace Web.ViewModels.GetList
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
