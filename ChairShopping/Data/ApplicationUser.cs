using Microsoft.AspNetCore.Identity;

namespace ChairShopping.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Country { get; set; } = "Egypt";
        public bool IsAgree { get; set; }
        public string ImageUrl { get; set; }
    }
}
