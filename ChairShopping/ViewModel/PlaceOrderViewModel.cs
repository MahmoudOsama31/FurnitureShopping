using ChairShopping.Models;
using System.ComponentModel.DataAnnotations;


namespace ChairShopping.ViewModel
{
	public class PlaceOrderViewModel
	{
		[Required(ErrorMessage ="Please enter your FirstName")]
		public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your Address to send order")]
        public string Address { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@gmail.com",ErrorMessage ="Please enter a valid email which ends with @gmail.com")]
		[Required(ErrorMessage ="Please enter your email to send email")]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone")]
        public string Phone { get; set; }
		public string OrderNotes { get; set; }
		public int? CouponId { get; set; } = null;
		public Coupon? Coupon { get; set; }
    }
}
