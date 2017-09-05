using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using System.Collections;

using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace CoreFamework.Security
{

    public sealed class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserLoginStore<User>
    {
        private SecurityContext _context;

        public UserStore(SecurityContext context)
        {
            _context = context;
        }

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            user.UserLogin.Add(new UserLogin()
            {
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });


            return _context.SaveChangesAsync();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);

            Int32 saveChangesResult = await _context.SaveChangesAsync();

            if (saveChangesResult != 0)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);

            Int32 saveChangesResult = await _context.SaveChangesAsync();

            if (saveChangesResult != 0)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
        }

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserLogin.Any(s => s.LoginProvider == loginProvider && s.ProviderKey == providerKey));
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            var userLoginInfos = user.UserLogin.Select(ul => new UserLoginInfo(ul.LoginProvider, ul.ProviderKey, ul.LoginProvider)).ToList();
            return await Task.FromResult<IList<UserLoginInfo>>(userLoginInfos);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.EntityGuid.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.ToString());
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {

            _context.Users.Remove(user);

            return (Task)_context.SaveChangesAsync();

        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return new Task(() => { user.UserName = userName; });
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Entry(user).State = EntityState.Modified;

            Int32 saveChangesResult = await _context.SaveChangesAsync();

            if (saveChangesResult != 0)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }

        }
    }

}
