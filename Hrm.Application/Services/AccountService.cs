using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class AccountService: IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, IRepository<User> userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task<Domain.ViewModels.Account.Profile?> GetProfileAsync(string id)
        {
            return await _userRepository
                .Query()
                .Include(x => x.Department)
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<Domain.ViewModels.Account.Profile>(x))
                .FirstOrDefaultAsync();
        }
    }
}
