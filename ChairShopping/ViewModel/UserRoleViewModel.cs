using ChairShopping.Data;

namespace ChairShopping.ViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationRole>? Roles { get; set; }
        public IEnumerable<ApplicationUser>? Users { get; set; }
    }
}
