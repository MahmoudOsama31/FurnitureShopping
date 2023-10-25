using System.ComponentModel.DataAnnotations;

namespace ChairShopping.Models
{
	public class ContactUs
	{
		public int Id { get; set; }
		[Required]
		public string email { get; set; }
		[Required]
		public string message { get; set; }
	}
}
