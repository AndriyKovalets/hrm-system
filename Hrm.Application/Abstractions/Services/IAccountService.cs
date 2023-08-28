
using Hrm.Domain.ViewModels.Account;

namespace Hrm.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task AddUserPasswordAsync(string email, string password);
        Task<Profile?> GetProfileAsync(string id);
    }
}
