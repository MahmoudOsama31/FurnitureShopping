using System.ComponentModel.DataAnnotations;

namespace ChairShopping.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "please Insert Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
