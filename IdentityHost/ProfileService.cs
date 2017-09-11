using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityHost
{
	public class ProfileService : IProfileService
	{
		MyUserManager _myUserManager;
		public ProfileService()
		{
			_myUserManager = new MyUserManager();
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var sub = context.Subject.FindFirst("sub")?.Value;
			if (sub != null) {
				var user = await _myUserManager.FindByNameAsync(sub);
				var cp = await getClaims(user);

				var claims = cp.Claims;
				if (context.AllClaimsRequested == false ||
					(context.RequestedClaimTypes != null && context.RequestedClaimTypes.Any())) {
					claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToArray().AsEnumerable();
				}

				context.IssuedClaims = claims;
			}
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			return Task.FromResult(0);
		}

		private async Task<ClaimsPrincipal> getClaims(MyUser user)
		{
			if (user == null) {
				throw new ArgumentNullException(nameof(user));
			}

			var id = new ClaimsIdentity();
			id.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));

			id.AddClaims(await _myUserManager.GetClaimsAsync(user));

			return new ClaimsPrincipal(id);
		}

	}
}
