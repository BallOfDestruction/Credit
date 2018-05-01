using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.GetList;

namespace Web.Controllers
{
    public class CreditController : CommonController
    {
        [HttpGet]
        public JsonResult GetList()
        {
            var authorization = GetUserWithToken();
            if(authorization.Error != null) return Json(authorization.Error);

            var credits = Context.Credits.Where(w => w.UserId == authorization.User.Id)?.ToList();

            return Json(new GetListResponce(credits));
        }
    }
}