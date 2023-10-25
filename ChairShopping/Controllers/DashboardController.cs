using ChairShopping.Data;
using ChairShopping.Interfaces;
using ChairShopping.Models;
using ChairShopping.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChairShopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdmin _repo;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(IAdmin repo, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _repo.GetAllOrders();
            var products = await _repo.GetAllProducts();
            var categories = await _repo.GetAllCategories();
            var users = await _repo.GetAllUsers();
            var coupons = await _repo.GetAllCoupons();
            var placeOrder = await _repo.GetAllPlaceOrders();
            var fav = await _repo.GetAllFavourits();
            var model = new DashboardViewModel
            {
                Category = categories,
                Order = orders,
                Product = products,
                Users = users,
                Coupons = coupons,
                PlaceOrders = placeOrder,
                Favourites = fav
            };
            return View(model);
        }
        public async Task<IActionResult> GetAllUsers()
        {
            ViewBag.AllUsers = await _repo.GetAllUsers();
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> AddUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddUser(model);
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<ApplicationUser>> EditUser(string id)
        {
            if (id != null)
            {
                var user = await _repo.GetUserById(id);
                if (user != null)
                {
                    ViewBag.UpdateUser = user;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> EditUser(RegisterVM model, string id)
        {
            await _repo.EditUser(model, id);
            return RedirectToAction("GetAllUsers", "Dashboard");
        }
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (id != null)
            {
                var user = await _repo.GetUserById(id);
                if (user != null)
                {
                    ViewBag.UpdateUser = user;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> DeleteUser(string id, RegisterVM model)
        {
            await _repo.DeleteUser(id);
            return RedirectToAction("GetAllUsers", "Dashboard");
        }
        public async Task<ActionResult<ApplicationUser>> GetUserDetails(string id)
        {
            if (id != null)
            {
                var user = await _repo.GetUserById(id);
                if (user != null)
                {
                    ViewBag.UpdateUser = user;
                }
            }
            return View();
        }
        public async Task<IActionResult> GetAllRoles()
        {
            ViewBag.AllRoles = await _repo.GetAllRoles();
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationRole>> AddRole(ApplicationRole model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddRole(model);
                return RedirectToAction("GetAllRoles");
            }
            return View();
        }
        public async Task<ActionResult<ApplicationRole>> EditRole(string id)
        {
            if (id != null)
            {
                var role = await _repo.GetRoleById(id);
                if (role != null)
                {
                    ViewBag.Updaterole = role;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationRole>> EditRole(ApplicationRole model, string id)
        {
            await _repo.EditRole(model, id);
            return RedirectToAction("GetAllRoles", "Dashboard");
        }
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (id != null)
            {
                var role = await _repo.GetRoleById(id);
                if (role != null)
                {
                    ViewBag.Updaterole = role;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationRole>> DeleteRole(string id, ApplicationRole model)
        {
            await _repo.DeleteRole(id);
            return RedirectToAction("GetAllRoles", "Dashboard");
        }
        public async Task<ActionResult<ApplicationRole>> GetRoleDetails(string id)
        {
            if (id != null)
            {
                var role = await _repo.GetRoleById(id);
                if (role != null)
                {
                    ViewBag.Updaterole = role;
                }
            }
            return View();
        }

        public async Task<ActionResult<IEnumerable<UserRoleViewModel>>> UserRoles()
        {
            var userRoles = await _repo.GetAllUserRole();
            if (userRoles != null)
            {
                ViewBag.userRoles = userRoles;
            }
            return View();
        }
        public async Task<IActionResult> AddUserRole()
        {
            var model = new UserRoleViewModel
            {
                Roles = await _roleManager.Roles.ToListAsync(),
                Users = await _repo.GetAllUsers()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<UserRoleViewModel>> AddUserRole(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddUserRole(model);
                return RedirectToAction("UserRoles");
            }
            return View();
        }
        public async Task<IActionResult> EditUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.userRole = user;
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var model = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    RoleName = userRoles.FirstOrDefault(),
                    Roles = _roleManager.Roles.ToList()
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult<UserRoleViewModel>> EditUserRole(UserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _repo.EditUserRole(model);
            return RedirectToAction("UserRoles");
        }
        public async Task<IActionResult> DeleteUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.userRoleDelete = user;
            if (user != null)
            {
                var model = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                return View(model);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(UserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _repo.DeleteUserRole(model);
            return RedirectToAction("UserRoles");
        }

        public async Task<IActionResult> GetAllCategories()
        {
            ViewBag.AllCategories = await _repo.GetAllCategories();
            return View(await _repo.GetCategoryInSinglePage(1));
        }
        [HttpPost]
        public async Task<IActionResult> GetAllCategories(int currentPageIndex)
        {
            ViewBag.AllCategories = await _repo.GetAllCategories();
            return View(await _repo.GetCategoryInSinglePage(currentPageIndex));
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddCategory(model);
                return RedirectToAction("GetAllCategories");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<Category>> EditCategory(int id)
        {
            if (id > 0)
            {
                var Category = await _repo.GetCategoryById(id);
                if (Category != null)
                {
                    ViewBag.UpdateCategory = Category;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Category>> EditCategory(Category model, int id)
        {
            await _repo.EditCategory(model, id);
            return RedirectToAction("GetAllCategories", "Dashboard");
        }
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var cat = await _repo.GetCategoryById(id);
                if (cat != null)
                {
                    ViewBag.Updatecat = cat;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Category>> DeleteCategory(int id, Category model)
        {
            await _repo.DeleteCategory(id);
            return RedirectToAction("GetAllCategories", "Dashboard");
        }
        public async Task<ActionResult<Category>> GetCategoryDetails(int id)
        {
            if (id > 0)
            {
                var cat = await _repo.GetCategoryById(id);
                if (cat != null)
                {
                    ViewBag.Updatecat = cat;
                }
            }
            return View();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetAllProducts()
        {
            ViewBag.AllProducts = await _repo.GetAllProducts();
            return View();
        }
        public async Task<IActionResult> AddProduct()
        {
            var Subproducts = await _repo.GetAllCategories();
            var model = new ProductViewModel
            {
                categoriesList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddProduct(model);
                TempData["ProductAdded"] = "Product Added Sucessfully";
                return RedirectToAction("GetAllProducts");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<Product>> EditProduct(int id)
        {
            var Subproducts = await _repo.GetAllCategories();
            var model = new ProductViewModel
            {
                categoriesList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList()
            };
            if (id > 0)
            {
                var prd = await _repo.GetProductById(id);
                if (prd != null)
                {
                    ViewBag.UpdateProduct = prd;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> EditProduct(ProductViewModel model, int id)
        {
            await _repo.EditProduct(model, id);
            return RedirectToAction("GetAllProducts", "Dashboard");
        }
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            if (id > 0)
            {
                var prd = await _repo.GetProductById(id);
                if (prd != null)
                {
                    ViewBag.Updateprd = prd;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> DeleteProduct(int id, Product model)
        {
            await _repo.DeleteProduct(id);
            return RedirectToAction("GetAllProducts", "Dashboard");
        }
        public async Task<ActionResult<Product>> GetProductDetails(int id)
        {
            if (id > 0)
            {
                var prd = await _repo.GetProductById(id);
                if (prd != null)
                {
                    ViewBag.Updateprd = prd;
                }
            }
            return View();
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetAllOrders()
        {
            ViewBag.AllOrders = await _repo.GetAllOrders();
            return View();
        }
        public async Task<IActionResult> AddOrder()
        {
            var Subproducts = await _repo.GetAllProducts();
            var subUsers = await _repo.GetAllUsers();
            var model = new OrderViewModel
            {
                productList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ProductName
                }).ToList(),
                userList = subUsers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.UserName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddOrder(model);
                return RedirectToAction("GetAllOrders");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<Order>> EditOrder(int id)
        {
            var Subproducts = await _repo.GetAllProducts();
            var subUsers = await _repo.GetAllUsers();
            var model = new OrderViewModel
            {
                productList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ProductName
                }).ToList(),
                userList = subUsers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.UserName
                }).ToList()
            };
            if (id > 0)
            {
                var order = await _repo.GetOrderById(id);
                if (order != null)
                {
                    ViewBag.Updateorder = order;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Order>> EditOrder(OrderViewModel model, int id)
        {
            await _repo.EditOrder(model, id);
            return RedirectToAction("GetAllOrders", "Dashboard");
        }
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            if (id > 0)
            {
                var order = await _repo.GetOrderById(id);
                if (order != null)
                {
                    ViewBag.Deleteorder = order;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Order>> DeleteOrder(int id, Order model)
        {
            await _repo.DeleteOrder(id);
            return RedirectToAction("GetAllOrders", "Dashboard");
        }
        public async Task<ActionResult<Order>> GetOrderDetails(int id)
        {
            if (id > 0)
            {
                var order = await _repo.GetOrderById(id);
                if (order != null)
                {
                    ViewBag.orderDetail = order;
                }
            }
            return View();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetAllCoupons()
        {
            ViewBag.AllCoupons = await _repo.GetAllCoupons();
            return View();
        }
        public IActionResult AddCoupon()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Coupon>> AddCoupon(CouponViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddCoupon(model);
                return RedirectToAction("GetAllCoupons");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<Coupon>> EditCoupon(int id)
        {
            if (id > 0)
            {
                var coupon = await _repo.GetCouponsById(id);
                if (coupon != null)
                {
                    ViewBag.Updatecoupon = coupon;
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Coupon>> EditCoupon(CouponViewModel model, int id)
        {
            await _repo.EditCoupon(model, id);
            return RedirectToAction("GetAllCoupons", "Dashboard");
        }
        [HttpPost]
        public async Task<ActionResult<Coupon>> DeleteCoupon(int id, Coupon model)
        {
            await _repo.DeleteCoupon(id);
            return RedirectToAction("GetAllCoupons", "Dashboard");
        }
        public async Task<ActionResult<Coupon>> GetCouponDetails(int id)
        {
            if (id > 0)
            {
                var Coupon = await _repo.GetCouponsById(id);
                if (Coupon != null)
                {
                    ViewBag.CouponDetail = Coupon;
                }
            }
            return View();
        }
        ////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetAllPlaceOrders()
        {
            ViewBag.AllPlaceOrders = await _repo.GetAllPlaceOrders();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<PlaceOrder>> DeletePlaceOrder(int id, PlaceOrder model)
        {
            await _repo.DeletePlaceOrder(id);
            return RedirectToAction("GetAllPlaceOrders", "Dashboard");
        }
        ////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GetAllFavourits()
        {
            ViewBag.AllFavourits = await _repo.GetAllFavourits();
            return View();
        }
        public async Task<IActionResult> AddFavourits()
        {
            var Subproducts = await _repo.GetAllProducts();
            var subUsers = await _repo.GetAllUsers();
            var model = new FavouritsViewModel
            {
                productList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ProductName
                }).ToList(),
                userList = subUsers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.UserName
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Favourite>> AddFavourits(FavouritsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddFavourite(model);
                return RedirectToAction("GetAllFavourits");
            }
            else
            {
                ModelState.AddModelError("Key", "something wrong in validation");
            }
            return View();
        }
        public async Task<ActionResult<Favourite>> EditFavourits(int id)
        {
            var Subproducts = await _repo.GetAllProducts();
            var subUsers = await _repo.GetAllUsers();
            var model = new FavouritsViewModel
            {
                productList = Subproducts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ProductName
                }).ToList(),
                userList = subUsers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.UserName
                }).ToList()
            };
            if (id > 0)
            {
                var favourite = await _repo.GetFavouriteById(id);
                if (favourite != null)
                {
                    ViewBag.Updatefavourite = favourite;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult<Favourite>> EditFavourits(FavouritsViewModel model, int id)
        {
            await _repo.EditFavourite(model, id);
            return RedirectToAction("GetAllFavourits", "Dashboard");
        }
        [HttpPost]
        public async Task<ActionResult<Favourite>> DeleteFavourite(int id, Favourite model)
        {
            await _repo.DeleteFavourite(id);
            return RedirectToAction("GetAllFavourits", "Dashboard");
        }
        public async Task<ActionResult<Favourite>> GetFavouriteDetails(int id)
        {
            if (id > 0)
            {
                var Favourite = await _repo.GetFavouriteById(id);
                if (Favourite != null)
                {
                    ViewBag.FavouriteDetail = Favourite;
                }
            }
            return View();
        }
    }
}

