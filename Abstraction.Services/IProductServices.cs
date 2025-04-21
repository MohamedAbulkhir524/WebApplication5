using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
	public interface IProductServices
	{
		Task<IEnumerable<ProductsDtos>> GetAllProductsAsync();

		Task<ProductsDtos> GetProductByIdAsync(int id);

		Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

		Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
	}
}
