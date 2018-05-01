using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CommonController : Controller
    {
        protected Context Context { get; set; }

        protected (User User, Error Error) GetUserWithToken()
        {
            var headers = HttpContext.Request.Headers;

            if (headers.TryGetValue("Authorization", out var values))
            {
                var token = values.FirstOrDefault()?.Replace("Bearer ","");
                var tokenModel = Context.TokenModels.Include(w => w.User).FirstOrDefault(w => w.Token == token);
                if (tokenModel?.User == null)
                {
                    return (null, new Error("NotAuthorization", "Пользователь не авторизован"));
                }
                return (tokenModel.User, null);
            }
            return (null, new Error("NotAuthorization", "Пользователь не авторизован"));
        }
    }
}
