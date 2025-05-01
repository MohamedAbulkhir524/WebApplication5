using Domain.Entites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		
		private readonly StoreDbContext _context;
		private readonly StoreIdentityDbContext _identityDbContext;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<User> _userManager;



		public DbInitializer(StoreDbContext context, StoreIdentityDbContext IdentityDbContext, RoleManager<IdentityRole> roleManager,
			 UserManager<User> userManager
			)
		{
			_context = context;
			 _identityDbContext= IdentityDbContext;
			_roleManager= roleManager;
			_userManager = userManager;
		}
		public void Initialize()
		{
			if (!_context.ProductTypes.Any())
			{
				var typesData = File.ReadAllText(@"..\infastrcture\Persistence\Data\DataSeed\types.Json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

				if(types is not null && types.Any())
				{
					_context.ProductTypes.AddRange(types);
					_context.SaveChanges();
				}

			}

			if (!_context.ProductBrands.Any())
			{
				var brandsData = File.ReadAllText(@"..\infastrcture\Persistence\Data\DataSeed\brands.Json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (brands is not null && brands.Any())
				{
					_context.ProductBrands.AddRange(brands);
					_context.SaveChanges();
				}

			}


			if (!_context.Products.Any())
			{
				var productsData = File.ReadAllText(@"..\infastrcture\Persistence/Data\DataSeed\products.Json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products is not null && products.Any())
				{
					_context.Products.AddRange(products);
					_context.SaveChanges();
				}

			}
		}

		public Task InitiaLizeIdetityAsync()
		{
			if (!_identityDbContext.Database.GetPendingMigrations().Any())
			{
				await _identityDbContext.Database.MigrateAsync();
			}

			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
				await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
			}

			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
				await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
			}

			if (!_userManager.Users.Any())
			{
				var superAdminUser = new User
				{
					DisplayName = "Super Admin",
					Email = "superAdmin@gmail.com",
					UserName = "superAdmin",
					PhoneNumber = "1234567890"
				};

				var adminUser = new User
				{
					DisplayName = "Admin",
					Email = "Admin@gmail.com",
					UserName = "Admin",
					PhoneNumber = "987654321"
				};

				await _userManager.CreateAsync(superAdminUser, "Password");
				await _userManager.CreateAsync(adminUser, "Password");

				await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
				await _userManager.AddToRoleAsync(adminUser, "Admin");
			}
		}
}
