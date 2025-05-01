using Domain.Entites;
using Domain.Interfaces;
using Persistence.Data;
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

		public DbInitializer(StoreDbContext context)
		{
			_context = context;
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
	}
}
