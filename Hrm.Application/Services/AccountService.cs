using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hrm.Application.Services
{
    internal class AccountService: IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Claim>> LoginAsync(LoginModel loginModel)
        {
            IEnumerable<Claim>? claims = null;

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
            {
                throw new InvalidEmailOrPasswordExeption();
            }

            if(!await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                throw new InvalidEmailOrPasswordExeption();
            }
            var userRoles = await _userManager.GetRolesAsync(user);

            claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, userRoles.First()),
            };

            return claims;
        }

        public async Task AddUserPasswordAsync(string userId, string password)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                throw new UserNotFoundExeption();
            }

            if (await _userManager.HasPasswordAsync(user))
            {
                throw new UserAlreadyHasPassword();
            }

            await _userManager.AddPasswordAsync(user, password);
        }
    }
}
