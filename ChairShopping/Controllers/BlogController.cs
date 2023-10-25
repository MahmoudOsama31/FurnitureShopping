using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
