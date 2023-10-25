using ChairShopping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.ComponentsControllers
{
    [ViewComponent(Name = "ProductClasses")]
    public class ProductClasses : ViewComponent
    {
        private readonly IAdmin _productClasses;
        private readonly IProductFilter _filter;

        public ProductClasses(IAdmin productClasses , IProductFilter filter)
        {
            this._productClasses = productClasses;
            _filter = filter;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                if (categoryId == 0)  // this condition to load all categories when page is load
                {
                    return View("Index", await _productClasses.GetProductsLimitAsync(8));  // view 10 products to prevent load on server
                }
                else
                {
                    return View("Index", await _productClasses.GetProductsByCatgoryIdAsync(categoryId));
                }
            }
            else
            {
                var result = _productClasses.SearchProduct(search);
                ViewBag.Search = search;
                return View("Index", await result);
            }
        }
    }
}
