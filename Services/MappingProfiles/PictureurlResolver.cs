using AutoMapper;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
	internal class PictureurlResolver : IValueResolver<Product, ProductsDtos, string>
	{
		public string Resolve(Product source, ProductsDtos destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.PictureURl))
				return string.Empty;

			
				return$"{source.PictureURl}";


	    }
	}
}
