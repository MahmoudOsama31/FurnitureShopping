using ChairShopping.Models;

namespace ChairShopping.Interfaces
{
    public interface IProductFilter
    {
        //IEnumerable<Product> GetFilteredProducts(decimal minPrice, decimal maxPrice, string[] colors);
        IEnumerable<Product> GetFilteredProducts(string sortBy, string price, string color);

    }
}
