using ChairShopping.Data;

namespace ChairShopping.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsFavourite { get; set; }
    }
}
