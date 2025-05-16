using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DEPI_GraduationProject.Models
{
	public class Clients : User
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; } = null!;

		[Required]
		[Phone]
		public string Phone { get; set; } = null!;

		[Required]
		[MaxLength(20)]
		public string CarNumber { get; set; } = null!;

		// Navigation property for related Sales
		public ICollection<Sales> Sales { get; set; } = new List<Sales>();
	}
}
