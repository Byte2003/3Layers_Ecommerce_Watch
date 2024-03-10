using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Data;
using MVC_Watch_Data.Models;
using MVC_Watch_Data.Repositors;
using MVC_Watch_UI.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DependencyInjection
{
	public class DI
	{
        public void ConfigureServices(IServiceCollection service)
		{
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			IConfigurationBuilder configBuilder = null;
			if (environmentName == "Development")
			{
                 configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
            } else
			{
                 configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.Production.json");
            }
            
			var configurationroot = configBuilder.Build();
			// Add services to the container.
			service.AddControllers().AddJsonOptions(x =>
							x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
			service.AddControllersWithViews();
			service.AddRazorPages();
			//DBContext and Identity
			service.AddDbContext<AppDbContext>(option =>
			{
				var connection = configurationroot.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
				option.UseSqlServer(connection, b => b.MigrationsAssembly("MVC_Watch_Data"));
				option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			service.AddIdentity<AppUser, IdentityRole>()
					.AddEntityFrameworkStores<AppDbContext>()
					.AddDefaultTokenProviders()
					.AddDefaultUI();
			service.AddScoped<IUnitOfWork, UnitOfWork>();
			service.AddAutoMapper(typeof(AutoMapperProfiles));
			service.AddScoped<CategoryService>();
			service.AddScoped<BrandService>();
			service.AddScoped<ProductService>();
			service.AddScoped<ProductDiscountService>();
			service.AddScoped<StockService>();
			service.AddScoped<CartItemService>();
			service.AddScoped<DbInitialize>();
			service.AddScoped<AuthService>();
			service.AddScoped<PaymentService>();
			service.AddScoped<OrderHeaderService>();
			service.AddScoped<OrderDetailService>();
            service.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = $"/Authentication/Auth/Login";
				option.LogoutPath = $"/Authentication/Auth/Logout";
				option.AccessDeniedPath = $"/Authentication/Auth/AccessDenied";
			});

			service.AddDistributedMemoryCache();
            service.AddSession(config => {
                config.Cookie.Name = "MVC_Watch";
                config.IdleTimeout = new TimeSpan(0, 30, 0);
            });
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		}
	}
}
