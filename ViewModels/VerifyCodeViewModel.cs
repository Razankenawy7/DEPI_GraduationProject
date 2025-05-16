using System.ComponentModel.DataAnnotations;

namespace DEPI_GraduationProject.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verification code is required")]
        public string Code { get; set; } = string.Empty;
    }
}
