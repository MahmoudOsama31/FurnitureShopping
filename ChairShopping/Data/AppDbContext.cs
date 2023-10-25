using ChairShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChairShopping.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {               
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<PlaceOrder> placeOrders { get; set; }
        public DbSet<Favourite> favourites { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
