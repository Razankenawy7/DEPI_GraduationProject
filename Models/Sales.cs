using System;
using System.Collections.Generic;

namespace DEPI_GraduationProject.Models
{
	public class Sales
	{
		public int Id { get; set; }

		public int LocationId { get; set; }

		// Change ClientId to string to match Clients.Id (which is string)
		public string ClientId { get; set; } = null!;

		public DateTime SaleDate { get; set; }

		// Navigation properties
		public Clients Clients { get; set; } = null!;

		public ICollection<SalesDetails> SaleDetails { get; set; } = new List<SalesDetails>();

		public ICollection<AdhesiveUsage> AdhesiveUsages { get; set; } = new List<AdhesiveUsage>();

		public Location Location { get; set; } = null!;
	}
}
