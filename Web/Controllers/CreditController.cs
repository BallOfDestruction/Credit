using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using Web.ViewModels.GetList;

namespace Web.Controllers
{
    public class CreditController : CommonController
    {
        public CreditController(Context context)
        {
            Context = context;
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var authorization = GetUserWithToken();
            if(authorization.Error != null) return Json(authorization.Error);

            var credits = Context.Credits.Where(w => w.UserId == authorization.User.Id).ToList();

            return Json(new GetListResponce(credits));
        }
    }
}