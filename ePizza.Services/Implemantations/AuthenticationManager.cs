using ePizza.Entities.Concrete;
using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implemantations
{
	public class AuthenticationManager : IAuthenticationService
	{
		protected SignInManager<User> _signManager;
		protected UserManager<User> _userManager;
		protected RoleManager<Role> _roleManager;

		public AuthenticationManager(SignInManager<User> signManager, UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			_signManager = signManager;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public User AuthenticateUser(string userName, string password)
		{
			var result = _signManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false).Result;
			if (result.Succeeded)
			{
				var user = _userManager.FindByNameAsync(userName).Result;
				var roles = _userManager.GetRolesAsync(user).Result;
				user.Roles = roles.ToArray();
				return user;
			}
			return null;
		}

		public bool CreateUser(User user, string password)
		{
			var result = _userManager.CreateAsync(user, password).Result;
			if (result.Succeeded)
			{
				//projenin ilerleyen süreçlerinde burası dinamik olmalıdır, googleden asp idendity örneklerine bakabilirsiniz ödev.
				string role = "User";
				//string role = "Admin";
				var res = _userManager.AddToRoleAsync(user, role).Result;
				if (res.Succeeded)
				{
					return true;
				}
			}
			return false;
		}

		public User GetUser(string username)
		{
			return _userManager.FindByNameAsync(username).Result;
		}

		public async Task<bool> SingOut()
		{
			//SignOut yapısı hata mekanizması gönderir hale getirmelidir. Geriye muhakak bir değer dönmelidir ve bu değer işlemin hatalı olduğunu göstermelidir.

			await _signManager.SignOutAsync();
			return true;
		}
	}
}
