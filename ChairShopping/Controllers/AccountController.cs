using ChairShopping.Data;
using ChairShopping.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChairShopping.Controllers
{
    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<ApplicationRole> roleManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			_roleManager = roleManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM login)
		{
            await AddRoles();
            await CreateAdmin();
            if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(login.Email);
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
					if (result.Succeeded)
					{
                        if (await _roleManager.RoleExistsAsync("User"))
                        {
                            if (!await _userManager.IsInRoleAsync(user, "User") && !await _userManager.IsInRoleAsync(user, "Admin"))
                            {
                                await _userManager.AddToRoleAsync(user, "User");
                            }
                        }
                        return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Cannot Login Please try Again");
					}
				}

			}
			// If we reach this point, something went wrong, so redisplay the login form
			return View(login);
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(register.Email);
				if (user == null)
				{
                    var folderPath = Directory.GetCurrentDirectory() + "/wwwroot/images/UserImages";
                    var folderName = Path.GetFileName(register.UserImage.FileName);
                    var finalPath = Path.Combine(folderPath, folderName);
                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        await register.UserImage.CopyToAsync(stream);
                    }
                    var NewUser = new ApplicationUser
					{
						UserName = register.UserName,
						Email = register.Email,
						IsAgree = register.IsAgree,
						ImageUrl = register.UserImage.FileName

					};
					var result = await _userManager.CreateAsync(NewUser, register.Password);
					if (result.Succeeded)
					{
						TempData["UserAdded"] = "User Added Sucessfully";
						return RedirectToAction("Login", "Account");
					}
					else
					{
						foreach (var err in result.Errors)
						{
							ModelState.AddModelError(string.Empty, err.Description); // handle errors
						}
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "User Already Exist !!");
				}
			}
			// If we reach this point, something went wrong, so redisplay the login form
			return View(register);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return RedirectToAction("Login", "Account");
		}
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPassword)
		{
			//if (ModelState.IsValid)
			//{
			//	var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
			//	if (user != null)
			//	{
			//		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			//		var callbackUrl = Url.Action("ResetPassword", "Account",
			//			new { userId = user.Id, token = token }, Request.Scheme);

			//		// Send the password reset link to the user's email
			//		// You can use any email sending mechanism here
			//		IEmailService _emailSender = new GmailEmailSender();
			//		await _emailSender.SendEmailAsync(forgetPassword.Email, "Reset Password",
			//			$"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

			//		return RedirectToAction("ForgotPasswordConfirmation");
			//	}
			//	else
			//	{
			//		ModelState.AddModelError(string.Empty, "Email doesn't  Exist !!");
			//	}
   //                 return RedirectToAction("ForgotPasswordConfirmation");
   //             }
   //             else
   //             {
   //                 ModelState.AddModelError(string.Empty, "Email doesn't  Exist !!");
   //             }
            

			// If we got this far, something failed, redisplay the form
			return View(forgetPassword);
		}

	
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        // Password reset successful
                        return RedirectToAction("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    // User not found
                    ModelState.AddModelError("", "Invalid password reset request.");
                }
            }

            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public async Task AddRoles()
        {
            if (_roleManager.Roles.Count() < 1)
            {
                var role = new ApplicationRole
                {
                    Name = "Admin",
                };
                await _roleManager.CreateAsync(role);
                role = new ApplicationRole
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(role);
            }
        }
        public async Task CreateAdmin()
        {
            var admin = await _userManager.FindByEmailAsync("Admin@gmail.com");
            if (admin == null)
            {
                var ad = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(ad, "123456");
                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync("Admin"))
                        await _userManager.AddToRoleAsync(ad, "Admin");
                }
            }
        }

    }
}
