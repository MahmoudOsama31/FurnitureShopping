using ChairShopping.Interfaces;
using ChairShopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChairShopping.Controllers
{
    [Authorize]  // use this to force user to login if session is expired
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdmin _repo;
        private readonly ICart _cart;

        public HomeController(ICart cart,ILogger<HomeController> logger,IAdmin repo)
        {
            _logger = logger;
            _repo = repo;
            _cart = cart;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<ActionResult<List<Product>>> Search(string search)
        //{
        //    var products = await _repo.SearchProduct(search);
        //    if (products == null)
        //    {
        //        return null;
        //    }
        //    return products;
        //}
        [HttpPost]
        public async Task<ActionResult<Order>> RemoveOrder(int id)
        {
            var order = await _repo.GetOrderById(id);
            var orders = await _cart.GetCartById(order.UserId);
            var totalorders = await _cart.TotalOrderPrice(order.UserId);
            var Final = totalorders-order.TotalPrice;
            ViewBag.UserId = order.UserId;
            await _cart.RemoveFromCart(id);
            return Json(new { success = true , cartDetails = $"{orders.Count() - 1},{order.Id},{Final}"});
        }
        [HttpPost]
        public async Task<ActionResult<Favourite>> RemoveFavourite(int id)
        {
            var favourite = await _repo.GetFavouriteById(id);
            var favourites = await _cart.GetFavouriteById(favourite.UserId);
            ViewBag.FavUserID = favourite.UserId;
            await _cart.RemoveFromFavourite(id);
            return Json(new { success = true, cartDetails = $"{favourites.Count() - 1},{favourite.Id}"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult ProductClasses(string categ_id,string search)
        {
            int categoryId = Convert.ToInt32(categ_id);
            return ViewComponent("ProductClasses", new { categoryId, search });
        }
    }
}