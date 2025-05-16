using System;
using System.ComponentModel.DataAnnotations;

namespace DEPI_GraduationProject.Models
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Phone { get; set; } = null!;

		[Required]
		public string Username { get; set; } = null!;

		[Required]
		public string PasswordHash { get; set; } = null!;

		[Required]
		public string Role { get; set; } = null!;

		public int? LocationId { get; set; }  // Correct property name

		public bool IsActive { get; set; }
	}
}
