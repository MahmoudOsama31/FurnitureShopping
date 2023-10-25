using ChairShopping.Data;
using System.ComponentModel.DataAnnotations;

namespace ChairShopping.ViewModel
{
    public class ForgetPasswordVM:ApplicationUser
    {
        [Required]
        public string Email { get; set; }
    }
}
