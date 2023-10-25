using ChairShopping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
	public class ShopController : Controller
	{
		private readonly IAdmin _admin;
        private readonly IProductFilter _filter;

        public ShopController(IAdmin admin , IProductFilter filter)
		{
			this._admin = admin;
            _filter = filter;
        }
        //public IActionResult Index(decimal? minPrice, decimal? maxPrice, string[] colors)
        //{
        //	if (minPrice == null && maxPrice == null && colors.Count()==0)
        //	{
        //              var result = _admin.GetAllProducts().Result.Take(8).ToList();
        //              return View(result);
        //  }
        //          var filteredProducts = _filter.GetFilteredProducts(minPrice ?? 0, maxPrice ?? 20000, colors);
        //          return View(filteredProducts);
        //  }

        public IActionResult Index(string sortBy, string price, string color)
        {
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(price) && string.IsNullOrEmpty(color))
            {
                var result = _admin.GetAllProducts().Result.Take(8).ToList();
                return View(result);
            }
            var filteredProducts = _filter.GetFilteredProducts(sortBy, price, color);
            return View(filteredProducts);
        }

    }
}
