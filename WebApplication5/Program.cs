
using Domain.Contracts;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using Services.MappingProfiles;
using System.Reflection;
using System.Reflection.Metadata;

namespace WebApplication5
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<StoreDbContext>(options =>
			{

				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQlConnection"));
			});
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IunitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IServicesManager, ServicesManager>();

			builder.Services.AddAutoMapper(x=>x.AddProfile(new ProductProfile()));
		
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();

			static void SeedDb(WebApplication app)
			{
				using var scope = app.Services.CreateScope();

				var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
				dbInitializer.Initialize();
			}
		}
	}
}
