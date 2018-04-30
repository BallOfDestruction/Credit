using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.ViewModels.Cms.Autorization;

namespace Web.Controllers.cms
{
    public class LoginCmsController : CommonController
    {
        public LoginCmsController(Context context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CmsLoginViewModel cmsLoginViewModel)
        {
            var admin = Context.Admins.FirstOrDefault(w => string.Equals(w.Login, cmsLoginViewModel.Login, StringComparison.CurrentCultureIgnoreCase) && w.Password == cmsLoginViewModel.Password);
            if (admin == null)
            {
                return RedirectToAction("Index");
            }

            await Authenticate(cmsLoginViewModel.Login);
            return RedirectToAction("GetList", "HomeCms", new {type = "Production"});
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}