using System.ComponentModel.DataAnnotations;

public class ForgetPasswordModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Registered Email Address")]
    public string Email { get; set; }
    public bool EmailSent { get; set; }
}
