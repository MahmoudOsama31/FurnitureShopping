using ChairShopping.Data;
using ChairShopping.Models;

namespace ChairShopping.ViewModel
{
    public class DashboardViewModel
    {
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }
        public IEnumerable<PlaceOrder> PlaceOrders { get; set; }
        public IEnumerable<Favourite> Favourites { get; set; }

    }
}
