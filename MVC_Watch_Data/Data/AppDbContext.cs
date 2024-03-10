using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Watch_Data.Models;

namespace MVC_Watch_Data.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
		{

		}
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductDiscount> ProductsDiscount { get; set; }
		public DbSet<Stock> Stocks { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);
			// Remove prefix AspNet of tables IdentityDbContext: AspNetUserRoles, AspNetUser ...
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}

			//Seeding roles for authentication
			var customerRoleID = "d4bbab4c-df0d-4621-bf96-5520388de0da";
			var adminRoleID = "3f0007dd-8403-4f3a-b5f8-0616bf668d0f";
			var roles = new List<IdentityRole>()
			{
				new IdentityRole()
				{
					Id= customerRoleID,
					ConcurrencyStamp = customerRoleID,
					Name= "customer",
					NormalizedName = "customer".ToUpper(),
				},
				new IdentityRole()
				{
					Id= adminRoleID,
					ConcurrencyStamp = adminRoleID,
					Name = "admin",
					NormalizedName = "admin".ToUpper()
				}
			};
			builder.Entity<IdentityRole>().HasData(roles);

			// Seeding admin account
			var appUser = new AppUser()
			{
				Id = "e20b0397-b30f-457c-a489-6cc1297cc8cd",
				UserName = "Hoang Truong",
				FirstName = "Hoang",
				LastName = "Truong",
				Email = "byte050403@gmail.com",
				Address = "Le Van Hien",
				City = "Da Nang",
				PostalCode = "12345",
				PhoneNumber = "0981995925"
			};
			PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
			appUser.PasswordHash = ph.HashPassword(appUser, "Bai0981995925!");
			builder.Entity<AppUser>().HasData(appUser);

			//set Admin role for this account
			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleID,
				UserId = "e20b0397-b30f-457c-a489-6cc1297cc8cd"
			});
		}
	}

}
