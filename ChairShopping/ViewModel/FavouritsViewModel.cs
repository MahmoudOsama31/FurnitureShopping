using ChairShopping.Data;
using ChairShopping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChairShopping.ViewModel
{
    public class FavouritsViewModel
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsFavourite { get; set; } = true;
        public List<SelectListItem> productList { get; set; }
        public List<SelectListItem> userList { get; set; }
    }
}
