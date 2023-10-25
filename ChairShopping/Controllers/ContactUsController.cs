using ChairShopping.Interfaces;
using ChairShopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
    public class ContactUsController : Controller
	{
		private readonly IAdmin _admin;
		public ContactUsController(IAdmin admin)
		{
			this._admin = admin;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddContact(ContactUs contactUs)
		{
			if (ModelState.IsValid)
			{
				await _admin.AddContactUs(contactUs);
				TempData["ContactUsStatus"] = "Thanks For Message We are supporting you soon ";
				return RedirectToAction("Index", "ContactUs");
			}
			else
			{
				return NotFound("Please Fill Data");
			}
		}
	}
}
