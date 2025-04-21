using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ProductServices(IunitOfWork unitOfWork,IMapper mapper) : IProductServices
	{
		
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
		{
			var brands = await unitOfWork.GetRepoitory<ProductBrand, int>().GetAllAysnc();

			var mappBrands=mapper.Map<IEnumerable<BrandResultDto>>(brands);

			return mappBrands;
		}

		public async Task<IEnumerable<ProductsDtos>> GetAllProductsAsync()
		{
			var products=await unitOfWork.GetRepoitory<Product, int>().GetAllAysnc();

			var mappedProducts=mapper.Map<IEnumerable<ProductsDtos>>(products);

			return mappedProducts;	
		}

		public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
		{
			var types = await unitOfWork.GetRepoitory<ProductType, int>().GetAllAysnc();

			var mappedtypes = mapper.Map<IEnumerable<TypeResultDto>>(types);

			return mappedtypes;
		}

		public async Task<ProductsDtos> GetProductByIdAsync(int id)
		{
			var products=await unitOfWork.GetRepoitory<Product,int>().GetAllAysnc();

			var mappedProducts=mapper.Map<ProductsDtos>(products);

			return mappedProducts;
		}
	}
}
