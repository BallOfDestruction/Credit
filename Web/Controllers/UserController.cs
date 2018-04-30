using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class UserController : CommonController
    {
        public UserController(Context context)
        {
            Context = context;
        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            var localEmail = email.ToLower();
            var user = Context.Users.FirstOrDefault(w => w.Email.ToLower() == localEmail && w.Password == password);

            if (user == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Json(new Error("code", "Пользователь не существует"));
            }

            var token = Guid.NewGuid().ToString();
            var tokenModel = new TokenModel() {Token = token, UserId = user.Id};
            Context.Add(tokenModel);

            return Json(new LoginResponceViewModel()
            {
                Token = token,
                Email = user.Email,
                Name = user.Name,
                SecondName = user.SecondName,
                Town = user.Town,
            });
        }
    }
}
