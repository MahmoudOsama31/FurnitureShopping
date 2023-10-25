using ChairShopping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
	public class ProductDetailsController : Controller
	{
        private readonly IAdmin _repo;
        public ProductDetailsController(IAdmin admin)
        {
			this._repo = admin;
				
        }
        public async Task<IActionResult> Index(int id)
		{
			var prod = await _repo.GetProductById(id);
			return View(prod);
		}
	}
}
