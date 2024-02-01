using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.DTO.LoginDTO;
using MVC_Watch_Business.DTO.RegisterDTO;
using MVC_Watch_Data.Data;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC_Watch_Business.Services
{
	public class AuthService
	{
		public const string AuthKey = "Auth";
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserStore<AppUser> _userStore;
		private readonly IUserEmailStore<AppUser> _emailStore;
		private readonly IEmailSender _emailSender;

		public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
			IUserStore<AppUser> userStore, IEmailSender emailSender)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_userStore = userStore;
			_emailSender = emailSender;
			_emailStore = GetEmailStore();
		}

		public async Task Login(LoginRequestDTO request)
		{
			// Clear the existing external cookie to ensure a clean login process
			await _signInManager.SignOutAsync();
			var user = await _userManager.FindByEmailAsync(request.Username);
			if (user is not null)
			{
				var passwordResult = await _userManager.CheckPasswordAsync(user, request.Password);

				if (passwordResult is true)
				{					
					await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, true);
				}
			}
		}

		public async Task Register(RegisterRequestDTO req)
		{
			var user = CreateUser();

			// Update field for user
			await _userStore.SetUserNameAsync(user, req.Email, CancellationToken.None);
			await _emailStore.SetEmailAsync(user, req.Email, CancellationToken.None);
			user.Address = req.Address;
			user.City = req.City;
			user.PostalCode = req.PostalCode;
			user.FirstName = req.FirstName;
			user.LastName = req.LastName;
			user.PhoneNumber = req.PhoneNumber;
			user.PhoneNumberConfirmed = false;
			var result = await _userManager.CreateAsync(user, req.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "customer");
				await _signInManager.PasswordSignInAsync(user.UserName, req.Password, true, true);
			}
		}
		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}
		private AppUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<AppUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
					$"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		private IUserEmailStore<AppUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<AppUser>)_userStore;
		}


	}
}

