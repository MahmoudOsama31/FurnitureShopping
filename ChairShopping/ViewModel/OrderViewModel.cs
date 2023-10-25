using ChairShopping.Data;
using ChairShopping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;

namespace ChairShopping.ViewModel
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public List<SelectListItem> productList { get; set; }
        public List<SelectListItem> userList { get; set; }

        //private decimal Total()
        //{
        //  return TotalPrice = (Price * Quantity) - Discount;
        //}
    }
}
