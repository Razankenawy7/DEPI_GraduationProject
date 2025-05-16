using System;
using Microsoft.AspNetCore.Identity;

namespace DEPI_GraduationProject.Models
{
	public class User : IdentityUser
	{
		public string? VerificationCode { get; set; }
		public DateTime? CodeGeneratedAt { get; set; }
	}
}
