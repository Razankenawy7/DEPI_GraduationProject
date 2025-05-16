using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DEPI_GraduationProject.ViewModels;
using DEPI_GraduationProject.Models;

namespace DEPI_GraduationProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // ========== Forgot Password ==========
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Please enter your email.");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Auth", new { email = user.Email, token }, Request.Scheme);

                await _emailSender.SendEmailAsync(email, "Reset Your Password",
                    $"Please reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>");
            }

            // Always show this message regardless of email existence to avoid user enumeration
            ViewBag.Message = "If your email is registered, you’ll receive a password reset link.";
            return View();
        }

        // ========== Reset Password ==========
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return BadRequest("A valid password reset token and email must be supplied.");
            }

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid password reset attempt.");
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password has been reset successfully. Please login.";
                return RedirectToAction("Login", "Auth");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // ========== Login ==========
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ========== Test View (Optional) ==========
        [HttpGet]
        public IActionResult TestView()
        {
            return View("ForgotPassword");
        }

        // ========== Create Test User (for development only) ==========
        [HttpGet]
        public async Task<IActionResult> CreateTestUser()
        {
            var user = new User
            {
                UserName = "razankenawy77@gmail.com",
                Email = "razankenawy77@gmail.com"
            };

            var result = await _userManager.CreateAsync(user, "Razan2003@");

            if (result.Succeeded)
            {
                return Content("User created successfully!");
            }
            else
            {
                return Content("Error: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
