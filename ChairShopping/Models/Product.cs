using System.ComponentModel.DataAnnotations.Schema;

namespace ChairShopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductCreation { get; set; } = DateTime.Now;
        public string ProductDescription { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public int NumberOfStock { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
