using ChairShopping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ChairShopping.ViewModel
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductCreation { get; set; } = DateTime.Now;
        public string ProductDescription { get; set; }
        public IFormFile Image { get; set; }
        public string Color { get; set; }
        public int NumberOfStock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<SelectListItem> categoriesList { get; set; }
        public string Search { get; set; }
    }
}
