using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Watch_UI.Mappings;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Data;
using MVC_Watch_Data.Models;
using MVC_Watch_Data.Repositors;
using MVC_Watch_Business.Services;
using System.Text.Json.Serialization;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var DI = new DI();
DI.ConfigureServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
SeedData();
app.MapRazorPages();
app.MapControllerRoute(
	name: "default",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
async void SeedData()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInit = scope.ServiceProvider.GetRequiredService<DbInitialize>();
		await dbInit.SeedAdminAccount();
	}
}
