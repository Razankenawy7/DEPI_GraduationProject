using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DEPI_GraduationProject.Models;

namespace DEPI_GraduationProject.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Clients> Clients { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Inventory> Inventory { get; set; }
		public DbSet<GlassFixationCategory> GlassFixationCategories { get; set; }
		public DbSet<Sales> Sales { get; set; }
		public DbSet<SalesDetails> SalesDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Inventory>()
				.HasOne(i => i.Product)
				.WithMany()
				.HasForeignKey(i => i.Product_id)
				.HasConstraintName("FK_Inventory_Product");

			modelBuilder.Entity<Inventory>()
				.HasOne(i => i.Location)
				.WithMany()
				.HasForeignKey(i => i.Location_id)
				.HasConstraintName("FK_Inventory_Location");

			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
				.HasConstraintName("FK_Product_Category");

			modelBuilder.Entity<Sales>()
				.HasOne(s => s.Clients)
				.WithMany(c => c.Sales)
				.HasForeignKey(s => s.ClientId)  // ClientId must be string in Sales model
				.HasConstraintName("FK_Sale_Client");

			modelBuilder.Entity<Sales>()
				.HasOne(s => s.Location)
				.WithMany()
				.HasForeignKey(s => s.LocationId)
				.HasConstraintName("FK_Sale_Location");

			modelBuilder.Entity<SalesDetails>()
				.HasOne(sd => sd.Sales)
				.WithMany(s => s.SaleDetails)
				.HasForeignKey(sd => sd.SaleId)
				.HasConstraintName("FK_SaleDetail_Sale");

			modelBuilder.Entity<SalesDetails>()
				.HasOne(sd => sd.Product)
				.WithMany()
				.HasForeignKey(sd => sd.ProductId)
				.HasConstraintName("FK_SaleDetail_Product");
		}
	}
}
