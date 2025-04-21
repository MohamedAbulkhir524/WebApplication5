using AutoMapper;
using Domain.Entites;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
	public class ProductProfile:Profile
	{
        public ProductProfile()
        {
            CreateMap<Product, ProductsDtos>()
                .ForMember(d => d.BrandName, option => option.MapFrom(src => src.productBrand.Name))
                .ForMember(d => d.TypeName, option => option.MapFrom(src => src.productType.Name))
                .ForMember(d => d.PictureURl, option => option.MapFrom<PictureurlResolver>());

            CreateMap<ProductType, TypeResultDto>();

            CreateMap<ProductBrand, BrandResultDto>();
        }
    }
}
