using ChairShopping.Data;
using ChairShopping.Models;
using ChairShopping.ViewModel;

namespace ChairShopping.Interfaces
{
    public interface IAdmin
    {
        public Task<IEnumerable<ApplicationUser>> GetAllUsers();
        public Task<ApplicationUser> AddUser(RegisterVM model);
        public Task<ApplicationUser> EditUser(RegisterVM model,string id);
        public Task<ApplicationUser> GetUserById(string id);
        public Task<ApplicationUser> DeleteUser(string id);
        ///////////////////////////////////////////////////////////////////
        public Task<IEnumerable<ApplicationRole>> GetAllRoles();
        public Task<ApplicationRole> AddRole(ApplicationRole model);
        public Task<ApplicationRole> EditRole(ApplicationRole model, string id);
        public Task<ApplicationRole> GetRoleById(string id);
        public Task<ApplicationRole> DeleteRole(string id);
        /////////////////////////////////////////////////////////////////
        public Task<IEnumerable<UserRoleViewModel>> GetAllUserRole();
        public Task<UserRoleViewModel> AddUserRole(UserRoleViewModel model);
        public Task<UserRoleViewModel> EditUserRole(UserRoleViewModel model);
        public Task<UserRoleViewModel> DeleteUserRole(UserRoleViewModel model);
        /////////////////////////////////////////////////////////////////////////
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<List<Category>> GetCategoriesLimitAsync(int limit);

        public Task<Category> AddCategory(Category model);
        public Task<Category> EditCategory(Category model, int id);
        public Task<Category> GetCategoryById(int id);
        public Task<Category> DeleteCategory(int id);
        public Task<CategoriesViewModel> GetCategoryInSinglePage(int currentPage);
        ////////////////////////////////////////////////////////////////////////////////
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<List<Product>> GetProductsLimitAsync(int limit);

        public Task<Product> AddProduct(ProductViewModel model);
        public Task<Product> EditProduct(ProductViewModel model, int id);
        public Task<Product> GetProductById(int id);
        public Task<Product> DeleteProduct(int id);
        public Task<List<Product>> GetProductsByCatgoryIdAsync(int id);
        public Task<List<Product>> SearchProduct(string search);
        ////////////////////////////////////////////////////////////////////////////////
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<Order> AddOrder(OrderViewModel model);
        public Task<Order> EditOrder(OrderViewModel model, int id);
        public Task<Order> GetOrderById(int id);
        public Task<Order> DeleteOrder(int id);
        ///////////////////////////////////////////////////////////////////////////////
        public Task<IEnumerable<Coupon>> GetAllCoupons();
        public Task<Coupon> GetCouponsById(int id);
        public Task<Coupon> AddCoupon(CouponViewModel model);
        public Task<Coupon> EditCoupon(CouponViewModel model, int id);
        public Task<Coupon> DeleteCoupon(int id);
		////////////////////////////////////////////////////////////////////////////////
		public Task<IEnumerable<PlaceOrder>> GetAllPlaceOrders();
		public Task<PlaceOrder> GetPlaceOrderById(int id);
		public Task<PlaceOrder> DeletePlaceOrder(int id);
        //add placeorder and edit placeorder ??!!!!?!?!?!?!? ////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        public Task<IEnumerable<Favourite>> GetAllFavourits();
        public Task<Favourite> GetFavouriteById(int id);
        public Task<Favourite> AddFavourite(FavouritsViewModel model);
        public Task<Favourite> EditFavourite(FavouritsViewModel model, int id);
        public Task<Favourite> DeleteFavourite(int id);
        //////////////////////////////////////////////////////////////////////////////////////////////
        public Task<ContactUs> AddContactUs(ContactUs contact);
	}
}
