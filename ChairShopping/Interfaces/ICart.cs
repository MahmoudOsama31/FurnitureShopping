using ChairShopping.Models;
using ChairShopping.ViewModel;

namespace ChairShopping.Interfaces
{
    public interface ICart
    {
        public decimal TotalPrice { get; set; }
        public  Task<IEnumerable<Order>> GetAllCarts();
        public Task<List<Order>> GetCartById(string id);
	    public Task<Order> AddToCart(OrderViewModel model);
        public Task<Order> RemoveFromCart(int id);
        public Task<Favourite> RemoveFromFavourite(int id);

        public Task<Order> UpdateCart(int id,OrderViewModel model);
        public Task<decimal> TotalOrderPrice(string Id);
        public Task<Favourite> AddToFavourite(FavouritsViewModel model);
        public Task<List<Favourite>> GetFavouriteById(string id);
    }
}
