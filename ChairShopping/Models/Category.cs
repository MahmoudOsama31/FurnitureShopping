namespace ChairShopping.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<Product> products { get; set; }
    }
}
