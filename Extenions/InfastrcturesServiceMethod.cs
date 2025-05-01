using Domain.Contracts;
using Domain.Entites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositres;
using Shared.IdentityDtos;
using StackExchange.Redis;
using System.Text;

namespace WebApplication5.Extenions
{
	public static class InfastrcturesServiceMethod
	{
		public static IServiceCollection AddInfastrcturesService(IServiceCollection Services,IConfiguration configuration)


		{
			Services.AddScoped<IDbInitializer, DbInitializer>();
			Services.AddScoped<IunitOfWork, UnitOfWork>();
			Services.AddScoped<IBasketRepositry, BasketRepositry>();

			Services.AddDbContext<StoreDbContext>(options =>
			{

				options.UseSqlServer(configuration.GetConnectionString("DefaultSQlConnection"));
			});

			Services.AddDbContext<StoreIdentityDbContext>(options =>
			{

				options.UseSqlServer(configuration.GetConnectionString("IdentitySQlConnection"));
			});

		   Services.AddSingleton<IConnectionMultiplexer>(
			 _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"))
			  );

			Services.ConfigureJwt(configuration);

			return Services;
		}

		public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
		{



		    services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 8;
				options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<StoreIdentityDbContext>();

			return services;
		}

		private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtConfig = configuration.GetSection("JwtOptions").Get<JwtOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtConfig.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey))
				};
			});
		}
	}
}
