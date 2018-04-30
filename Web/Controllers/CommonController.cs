using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CommonController : Controller
    {
        protected Context Context { get; set; }
    }
}
