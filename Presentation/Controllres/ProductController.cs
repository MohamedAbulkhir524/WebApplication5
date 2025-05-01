using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllres
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ProductController(IServicesManager servicesManager) :ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductsDtos>>> GetAllProducts()
		{
			var products=await servicesManager.ProductServices.GetAllProductsAsync();

			return Ok(products);
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductsDtos>>> GetProducts(int id)
		{
			var products = await servicesManager.ProductServices.GetProductByIdAsync(id);

			return Ok(products);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
		{
			var brands = await servicesManager.ProductServices.GetAllBrandsAsync();

			return Ok(brands);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
		{
			var types = await servicesManager.ProductServices.GetAllTypesAsync();

			return Ok(types);
		}



	}
}
