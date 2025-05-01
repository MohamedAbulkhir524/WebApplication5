using Domain.Contracts;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
	public class ProductwithfilterSpecifction : Spaceifcation<Product>
	{
		public ProductwithfilterSpecifction(int id) : base(p=>p.Id==id)
		{
			AddInclude.(Product => Product.ProductBrand);
			AddInclude.(Product => Product.ProductTypes);

		}

		public ProductwithfilterSpecifction(ProductSpecifcationparams specs)
			: base(product => (!specs.BrnadId.Hasvalue || product.BrandId == specs.BrandId)
			&& (!specs.TypeId.Hasvalue || product.TypeId == specs.TypeId) && (string.IsNullOrWhiteSpace(specs.Search) ||
			product.Name.ToLower().Contains.(specs.Search.ToLower().Trim()))
			)
		
        {
			AddInclude.(Product => Product.ProductBrand);
			AddInclude.(Product => Product.ProductTypes);

			if(specs.Sorting is not null)
			{
				switch(specs.Sorting)
				{
					case "nameAsc":
						SetOrderBy(product => product.Name);
						break;
					
					case "priceAsc":
						SetOrderByDescending(product => product.Price);
						break;
					
					case "priceDesc":
						SetOrderByDescending(product => product.Price);
						break;




				}
			}
		}
    }
}
