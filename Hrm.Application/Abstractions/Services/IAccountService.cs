using Hrm.Domain.Models.Account;
using System.Security.Claims;

namespace Hrm.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Claim>> LoginAsync(LoginModel loginModel);
        Task AddUserPasswordAsync(string userId, string password);
    }
}
