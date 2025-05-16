using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DEPI_GraduationProject.Models;

namespace DEPI_GraduationProject.Data
{
	public static class SeedData
	{
		public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			// Check if the Admin role exists; if not, create it
			if (!await roleManager.RoleExistsAsync("Admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
			}

			// Check if the admin user exists; if not, create it
			var defaultUser = await userManager.FindByEmailAsync("admin@admin.com");
			if (defaultUser == null)
			{
				var user = new User
				{
					UserName = "admin@admin.com",
					Email = "admin@admin.com",
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(user, "Admin@123");

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "Admin");
				}
				else
				{
					throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
				}
			}
		}
	}
}
