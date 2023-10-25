using System.ComponentModel.DataAnnotations;

namespace ChairShopping.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }
        [Required]
        public IFormFile UserImage { get; set; }


	}
}
