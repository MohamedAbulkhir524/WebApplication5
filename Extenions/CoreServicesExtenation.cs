using Services.Abstraction;
using Services.MappingProfiles;
using Services;
using Shared.IdentityDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication5.Extenions
{
	public static class CoreServicesExtenation
	{

		public static IServiceCollection AddCoreService(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddScoped<IServicesManager, ServicesManager>();

			services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));


			services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

			return services;
		}

		
	}
}
