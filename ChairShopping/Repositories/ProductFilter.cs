using ChairShopping.Data;
using ChairShopping.Interfaces;
using ChairShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ChairShopping.Repositories
{
    public class ProductFilter : IProductFilter
    {
        private readonly AppDbContext _db;

        public ProductFilter(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetFilteredProducts(string sortBy, string price, string color)
        {
            var filteredData = _db.products.AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                // Apply sorting logic based on the selected sortBy option
                // For example:
                if (sortBy == "Price: Low to High")
                {
                    filteredData = filteredData.OrderBy(item => item.Price);
                }
                else if (sortBy == "Price: High to Low")
                {
                    filteredData = filteredData.OrderByDescending(item => item.Price);
                }
            }
            if (!string.IsNullOrEmpty(price))
            {
                // Apply filtering logic based on the selected price range
                // For example:
                if (price == "0-50")
                {
                    filteredData = filteredData.Where(item => item.Price >= 0 && item.Price <= 50);
                }
                else if (price == "50-100")
                {
                    filteredData = filteredData.Where(item => item.Price >= 50 && item.Price <= 100);
                }
                else if (price == "100-150")
                {
                    filteredData = filteredData.Where(item => item.Price >= 100 && item.Price <= 150);
                }
                else if (price == "150-200")
                {
                    filteredData = filteredData.Where(item => item.Price >= 150 && item.Price <= 200);
                }
                else if(price == "200+")
                {
                    filteredData = filteredData.Where(item => item.Price > 200);
                }
                // Add more price range options as needed
            }
            if (!string.IsNullOrEmpty(color))
            {
                // Apply filtering logic based on the selected color
                filteredData = filteredData.Where(item => item.Color == color);
            }
            return filteredData.ToList();
        }
        //public IEnumerable<Product> GetFilteredProducts(decimal minPrice, decimal maxPrice, string[] colors)
        //{
        //    //queable to filter first
        //    var query = _db.products.AsQueryable();

        //    // Apply price filter
        //    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

        //    // Apply color filter
        //    if (colors != null && colors.Length > 0)
        //    {
        //        query = query.Where(p => colors.Contains(p.Color));
        //    }

        //    return query.ToList();
        //}

    }
}
