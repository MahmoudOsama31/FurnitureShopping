using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
