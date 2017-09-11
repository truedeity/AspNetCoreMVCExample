using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHost
{
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
	{
		private MyUserManager _myUserManager { get; set; }
		public ResourceOwnerPasswordValidator()
		{
			_myUserManager = new MyUserManager();
		}

		public async Task<CustomGrantValidationResult> ValidateAsync(string userName, string password, ValidatedTokenRequest request)
		{
			var user = await _myUserManager.FindByNameAsync(userName);
			if (user != null && await _myUserManager.CheckPasswordAsync(user, password)) {
				return new CustomGrantValidationResult(user.UserName, "password");
			}
			return new CustomGrantValidationResult("Invalid username or password");
		}
	}
}
