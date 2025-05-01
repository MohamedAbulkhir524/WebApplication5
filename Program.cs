
using Domain.Contracts;
using Domain.Entites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositres;
using Services;
using Services.Abstraction;
using Services.MappingProfiles;
using Shared.IdentityDtos;
using StackExchange.Redis;
using System.Reflection;
using System.Reflection.Metadata;
using WebApplication5.Extenions;
using WebApplication5.Middelware;

namespace WebApplication5
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			 
			builder.Services.AddInfastrcturesService.(builder.Configuration);
			builder.Services.AddCoreService.(builder.Configuration);



			//builder.Services.AddDbContext<StoreDbContext>(options =>
			//{

			//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQlConnection"));
			//});

			//builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
			//{

			//	options.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySQlConnection"));
			//});


			//builder.Services.AddSingleton<IConnectionMultiplexer>(
			//       _ => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
			//           );
			//builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			//builder.Services.AddScoped<IunitOfWork, UnitOfWork>();
			//builder.Services.AddScoped<IBasketRepositry ,BasketRepositry>();
			//builder.Services.AddScoped<IServicesManager, ServicesManager>();

			//builder.Services.AddAutoMapper(x=>x.AddProfile(new ProductProfile()));


			//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

			//ServicesesServices.AddIdentity<User, IdentityRole>(options =>
			//{
			//	options.Password.RequireNonAlphanumeric = false;
			//	options.Password.RequireLowercase = false;
			//	options.Password.RequireUppercase = false;
			//	options.Password.RequiredLength = 8;
			//	options.User.RequireUniqueEmail = true;
			//}).AddEntityFrameworkStores<StoreIdentityDbContext>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseMiddleware<GlobalerorrhandlingMiddelWares>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();

			static async void SeedDb(WebApplication app)
			{
				using var scope = app.Services.CreateScope();

				var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
				dbInitializer.Initialize();
				
			}
		}
	}
}
