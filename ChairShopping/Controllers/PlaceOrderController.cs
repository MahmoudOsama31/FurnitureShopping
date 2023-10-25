using ChairShopping.Data;
using ChairShopping.Interfaces;
using ChairShopping.Models;
using ChairShopping.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
    public class PlaceOrderController : Controller
    {
		private readonly SignInManager<ApplicationUser> _SignInManager;
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly ICart _cart;
		private readonly AppDbContext _db;
		private readonly IAdmin _repo;
        private readonly IEmailService _emailService;

        public PlaceOrderController(IEmailService emailService,ICart cart,AppDbContext db,IAdmin repo, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _db = db;
            _repo = repo;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
            _cart = cart;
            _emailService = emailService;
        }
		[HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
			if (_SignInManager.IsSignedIn(User))
			{
				var user = await _UserManager.GetUserAsync(User);
				ViewBag.user = user;
				var Total = await _cart.TotalOrderPrice(user.Id);
				ViewBag.totalOrder = Total;
			}
			var orders = await _cart.GetCartById(_UserManager.GetUserId(User));
			foreach (var order in orders)
			{
				if (_SignInManager.IsSignedIn(User))
				{
					var user = await _UserManager.GetUserAsync(User);
					if (user.Id == order.UserId)
					{
						ViewBag.order = orders;
					}
				}
			}
			var coupons = await _repo.GetAllCoupons();
			foreach (var item in coupons)
			{
				if (item.IsExpired == true)
				{
					await _repo.DeleteCoupon(item.Id);
				}
			}
			// fake solution ????????????????????!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			var Allcoupons = await _repo.GetAllCoupons();
			if (Allcoupons.Count()==0 || Allcoupons.Count()==1)
			{
				ViewBag.c = false;
			}
			else
			{
				ViewBag.c = true;
			}
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel model)
        {
			var coupons = await _repo.GetAllCoupons();
			if (coupons.Count()!=0 && coupons.Count()!=1)
			{
				foreach (var coupon in coupons)
				{
					if (coupon.CouponCode == model.Coupon.CouponCode)
					{
						model.CouponId = coupon.Id;
					}
				}
			}
            if (model.CouponId == null)
            {
                //make couponId = default coupon
                model.CouponId = 5;
            }
            if (ModelState.IsValid)
            {
				var orderPlace = new PlaceOrder
                {
                    Address = model.Address,
                    CouponId = model.CouponId,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    OrderNotes = model.OrderNotes,
                    Phone = model.Phone
                };
                var emailSubject = "Your Order From FurnitureWebsite";
                var emailBody = $"you orderd from FurnitureWebsite and we will come soon , follow us";
                var address = $"{model.Email}";
                //await _emailService.SendEmailAsync(model.Email, address, emailSubject, emailBody);
                await _db.placeOrders.AddAsync(orderPlace);
                await _db.SaveChangesAsync();
				if (_SignInManager.IsSignedIn(User))
				{
					var orders = await _repo.GetAllOrders();
					foreach (var order in orders)
					{
						var user = await _UserManager.GetUserAsync(User);
						if (order.UserId==user.Id)
						{
							order.Product.NumberOfStock -= order.Quantity;
                            await _repo.DeleteOrder(order.Id);
							await _db.SaveChangesAsync();
						}
					}
				}
                return RedirectToAction("ConfirmOrder");
			}
            return View(model);
        }
        public IActionResult ConfirmOrder()
        {
            return View();
        }

	}
}
