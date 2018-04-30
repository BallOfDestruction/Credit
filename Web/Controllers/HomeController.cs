using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : CommonController
    {
        public HomeController(Context context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
