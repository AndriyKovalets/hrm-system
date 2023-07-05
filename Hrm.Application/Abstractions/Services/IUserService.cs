using Hrm.Domain.Entities;

namespace Hrm.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user, string roleName, string? password = null);
    }
}
