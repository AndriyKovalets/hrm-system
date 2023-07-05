using Hrm.Application.Abstractions.Services;
using Hrm.Application.Roles;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Hrm.Application.Services
{
    internal class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateUserAsync(User user, string roleName, string? password = null)
        {
            var role = await _roleManager.FindByNameAsync(SystemRoles.Admin);

            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(SystemRoles.Admin));
            }

            if(await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                throw new UserEmailExistExeption();
            }

            if(string.IsNullOrEmpty(user.UserName))
            {
                user.UserName = user.Email;
            }

            var createUserResult = (password is not null)? await _userManager.CreateAsync(user, password): await _userManager.CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                StringBuilder errors = new StringBuilder();

                foreach (var error in createUserResult.Errors)
                {
                    errors.AppendLine(error.Description);
                }

                throw new UserCreateExeption(errors.ToString());
            }

            await _userManager.AddToRoleAsync(user, SystemRoles.Admin);
        }
    }
}
