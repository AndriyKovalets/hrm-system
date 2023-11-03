using Hrm.Domain.Entities;
using Hrm.Domain.ViewModels.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrm.Application.Abstractions.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(EmployeeModel user, string roleName, IFormFile? userImage = null, string? password = null);
        Task AddEmployeeToDepartamentAsync(int departmentId, params string[] userIds);
        Task AddManagerToDepartamentAsync(int departmentId, string? managerId);
        Task<IEnumerable<EmployeeShortInfoModel>> GetEmployeeShortInfoListAsync();
        Task<EmployeeFullInfoModel?> GetEmployeeFullInfoAsync(string id);
        Task<IEnumerable<SelectListItem>> GetEmployeesSelectListAsync(IEnumerable<string>? selectedUsers = null);
        Task ChangeUserRoleAsync(string userId, string userRole);
        Task<string?> GetCurrentUserRoleAsync(string userId);
        IEnumerable<SelectListItem> GetRoleSelectListAsync(string currentUserRole);
    }
}
