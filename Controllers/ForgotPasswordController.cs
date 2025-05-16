using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using DEPI_GraduationProject.Models;
using DEPI_GraduationProject.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DEPI_GraduationProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RequestReset() => View();

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestReset(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not found.");
                return View();
            }

            var code = new Random().Next(100000, 999999).ToString();
            user.VerificationCode = code;
            user.CodeGeneratedAt = DateTime.Now;
            _context.SaveChanges();

            await _emailSender.SendEmailAsync(email, "Password Reset Code", $"Your reset code is {code}");

            TempData["Email"] = email;
            return RedirectToAction("VerifyCode");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult VerifyCode() => View();

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyCode(string email, string code)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.VerificationCode == code);
            if (user == null || user.CodeGeneratedAt < DateTime.Now.AddMinutes(-15))
            {
                ModelState.AddModelError("", "Invalid or expired code.");
                return View();
            }

            TempData["Email"] = email;
            return RedirectToAction("ResetPassword");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword() => View();

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }

            // Hash new password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Update user password and clear verification code
            user.PasswordHash = hashedPassword;
            user.VerificationCode = null;
            user.CodeGeneratedAt = null;

            _context.SaveChanges();

            return RedirectToAction("Login", "Auth");
        }
    }
}
