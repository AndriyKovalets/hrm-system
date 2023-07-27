using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Microsoft.AspNetCore.Identity;

namespace Hrm.Application.Services
{
    internal class AccountService: IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserPasswordAsync(string email, string password)
        {

            var user = await _userManager.FindByEmailAsync(email);

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
