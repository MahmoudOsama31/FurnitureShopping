using ChairShopping.Data;
using ChairShopping.Interfaces;
using ChairShopping.Models;
using ChairShopping.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChairShopping.Controllers
{
    public class CartController : Controller
    {
		private readonly ICart _cart;
		private readonly IAdmin _repo;
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICart cart,IAdmin repo, AppDbContext db,UserManager<ApplicationUser> userManager)
        {
            _cart = cart;
            _repo=repo;
            _db = db;
           _userManager = userManager;
        }
        public async Task<IActionResult> GetCartById(string Id)
        {
            var user = await _repo.GetUserById(Id);
            if (user == null)
            {
                return null;
            }
            var orders =await _cart.GetCartById(Id);
            var user_Order = await _db.orders.Where(x => x.UserId == user.Id).ToListAsync();
            if (orders.Count()==0 && user_Order.Count()==0)
            {
                ViewBag.UserOrder = true;
            }
            else
            {
                ViewBag.UserOrder = false;
                ViewBag.Orders = orders;
            }
            var Total = await _cart.TotalOrderPrice(Id);
            ViewBag.Total = Total;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Order>> RemoveOrder(int id)
        {
            var order =await _repo.GetOrderById(id);
            ViewBag.UserId = order.UserId;
            await _cart.RemoveFromCart(id);
            return RedirectToAction("GetCartById","Cart",new {Id = ViewBag.UserId });
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddToCart(OrderViewModel model)
        {
            var product = await _repo.GetProductById(model.ProductId);
            var productUser = await _cart.GetCartById(model.UserId);
            if (product.NumberOfStock<model.Quantity)
            {
                TempData["cart_notAdded"] = $"We have just {product.NumberOfStock} from this product , or you orderd it already";
            }
            else
            {
                if (productUser.Count()==0)
                {
                    var order = await _cart.AddToCart(model);
                    ViewBag.UserId = order.UserId;
                    TempData["cart_added"] = "Product Added Sucessfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in productUser)
                    {
                        if (product.ProductName == item.Product.ProductName)
                        {
                            TempData["cart_OrderdJustOne"] = "You Orderd it already ,Go to your Cart";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                        var order = await _cart.AddToCart(model);
                        ViewBag.UserId = order.UserId;
                        TempData["cart_added"] = "Product Added Sucessfully";
                        return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult<Order>> UpdateCart(int id)
        {
            var Subproducts = await _repo.GetAllProducts();
            var subUsers = await _repo.GetAllUsers();
            var model = new OrderViewModel
            {
                productList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ProductName
                }).ToList(),
                userList = subUsers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.UserName
                }).ToList()
            };
            if (id > 0)
            {
                var order = await _repo.GetOrderById(id);
                if (order != null)
                {
                    ViewBag.Updateorder = order;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Order>> UpdateCart(OrderViewModel model, int id)
        {
            var order = await _repo.GetOrderById(id);
            if (order.Product.NumberOfStock < model.Quantity)
            {
                TempData["cart_notUpdated"] = $"We have just {order.Product.NumberOfStock} from this product , or you orderd it already";
                ViewBag.UserId = order.UserId;
                return RedirectToAction("GetCartById", "Cart", new { Id = ViewBag.UserId });
            }
            else
            {
                ViewBag.UserId = order.UserId;
                await _repo.EditOrder(model, id);
                return RedirectToAction("GetCartById", "Cart", new { Id = ViewBag.UserId });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Favourite>> AddFavourites(FavouritsViewModel model)
        {
            var favourite = await _cart.AddToFavourite(model);
            ViewBag.UserId = favourite.UserId;
            TempData["cart_added"] = "Product Favourite Added Sucessfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
