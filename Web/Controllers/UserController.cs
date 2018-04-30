using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.ViewModels;
using Web.ViewModels.Login;
using Web.ViewModels.Registration;

namespace Web.Controllers
{
    public class UserController : CommonController
    {
        public UserController(Context context)
        {
            Context = context;
        }

        [HttpPost]
        public JsonResult Login([FromBody]LoginRequest model)
        {
            var localEmail = model.Email.ToLower();
            var user = Context.Users.FirstOrDefault(w => w.Email.ToLower() == localEmail && w.Password == model.Password);

            if (user == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Json(new Error("code", "Такого пользователя не существует"));
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

        [HttpPost]
        public JsonResult Registration([FromBody]RegistrationRequest model)
        {
            var user = new User()
            {
                Name = model.Name,
                DateCreated = DateTime.Now,
                Email = model.Email,
                Password = model.Password,
                SecondName = model.SecondName,
                Town = model.Town,
            };
            Context.Add(user);
            Context.SaveChanges();

            return Json(new RegistrationResponce());
        }
    }
}
