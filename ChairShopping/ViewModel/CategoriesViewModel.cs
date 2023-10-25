using ChairShopping.Models;

namespace ChairShopping.ViewModel
{
    public class CategoriesViewModel
    {
        public List<Category> categories { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
