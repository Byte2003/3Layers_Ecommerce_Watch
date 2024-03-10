using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Data
{
	public class DbInitialize
	{
		private readonly AppDbContext _db;
		public DbInitialize(AppDbContext db)
		{
			_db = db;
		}
		public async Task SeedAdminAccount()
		{
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            var user = new AppUser
			{
				UserName = "byte050403@gmail.com",
				NormalizedUserName = "byte050403@gmail.com".ToUpper(),
				Email = "byte050403@gmail.com",
				NormalizedEmail = "byte050403@gmail.com".ToUpper(),
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
				Address = "Le Van Hien",
				City = "Da Nang",
				PostalCode = "12345",
				PhoneNumber = "0981995925"
			};
			var roleStore = new RoleStore<IdentityRole>(_db);
			if (!_db.Roles.Any(r => r.Name == "admin"))
			{
				await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
			}
			if (!_db.Users.Any(u => u.UserName == user.UserName))
			{
				var password = new PasswordHasher<AppUser>();
				var hashed = password.HashPassword(user, "Bai0981995925!");
				user.PasswordHash = hashed;
				var userStore = new UserStore<AppUser>(_db);
				await userStore.CreateAsync(user);
				await userStore.AddToRoleAsync(user, "admin");
			}

			await _db.SaveChangesAsync();
		}
	}
}
