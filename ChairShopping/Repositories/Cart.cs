using ChairShopping.Data;
using ChairShopping.Interfaces;
using ChairShopping.Models;
using ChairShopping.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ChairShopping.Repositories
{
	public class Cart : ICart
	{
		private readonly AppDbContext _db;
        private readonly IAdmin _repo;

        public Cart(AppDbContext db,IAdmin repo)
        {
			_db = db;
			_repo = repo;
        }

		public decimal TotalPrice { get; set; } = 0;

		public async Task<Order> AddToCart(OrderViewModel model)
		{
			if (model == null)
			{
				return null;
			}
			var order =  await _repo.AddOrder(model);
			return order;
		}

        public async Task<Favourite> AddToFavourite(FavouritsViewModel model)
        {
            if (model == null)
            {
                return null;
            }
            var favourite = await _repo.AddFavourite(model);
            return favourite;
        }

        public async Task<IEnumerable<Order>> GetAllCarts()
		{
			var orders= await _db.orders.Include(x => x.Product).Include(x => x.User).ToListAsync();
			return orders;
		}
		public async Task<List<Order>> GetCartById(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}
			List<Order> orders = await _db.orders.Where(x => x.UserId == id).Include(x => x.Product).Include(x => x.User).ToListAsync();
			return orders;
		}

        public async Task<List<Favourite>> GetFavouriteById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            List<Favourite> favourites = await _db.favourites.Where(x => x.UserId == id).Include(x => x.Product).Include(x => x.User).ToListAsync();
            return favourites;
        }

        public async Task<Order> RemoveFromCart(int id)
		{
			var order = await _repo.GetOrderById(id);
			if (order == null)
			{
				return null;
			}
			 _db.orders.Remove(order);
			await _db.SaveChangesAsync();
			return order;
		}

        public async Task<Favourite> RemoveFromFavourite(int id)
        {
            var fav = await _repo.GetFavouriteById(id);
            if (fav == null)
            {
                return null;
            }
            _db.favourites.Remove(fav);
            await _db.SaveChangesAsync();
            return fav;
        }

        public async Task<decimal> TotalOrderPrice(string Id)
		{
			var orders = await GetCartById(Id);
			foreach (var item in orders)
			{
				decimal SubTotal = item.TotalPrice;
				TotalPrice = TotalPrice + SubTotal;
			}
			return TotalPrice;
		}

        public async Task<Order> UpdateCart(int id, OrderViewModel model)
        {
			var order = await _repo.GetOrderById(id);
			if (order==null)
			{
				return null;
			}
			order.TotalPrice = model.TotalPrice;
		    order.Price = model.Price;
			order.OrderDate = DateTime.Now;
			order.Quantity = model.Quantity;
			order.Discount = model.Discount;
			order.UserId = model.UserId;
			order.ProductId = model.ProductId;
			_db.orders.Update(order);
			await _db.SaveChangesAsync();
			return order;
        }
    }
}
