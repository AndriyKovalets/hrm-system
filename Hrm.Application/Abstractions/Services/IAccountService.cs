namespace Hrm.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task AddUserPasswordAsync(string email, string password);
    }
}
