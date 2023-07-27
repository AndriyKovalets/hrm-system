using Hrm.Domain.Entities;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Departament;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrm.Application.Abstractions.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentModel> AddDepartamentAsync(DepartmentEditModel department);
        Task<IEnumerable<DepartmentShortInfoModel>> GetDepartamentShortInfoListAsync();
        Task AddParentDepartmnentAsync(int departmentId, int parentDepartmentId);
        Task<DepartmentFullInfoModel> GetDepartmentFullInfoAsync(int departmentId);
        Task<DepartmentModel> EditDepartmnentAsync(int departmentId, DepartmentEditModel department);
        Task<DepartmentEditModel> GetDepartmentForEditAsync(int departmentId);
        Task<IEnumerable<SelectListItem>> GetDepartamentSelectListAsync(int? excludeDepartmentId = null, int? selectDepartmentId = null);
        IEnumerable<SelectListItem> GetDepartamentRoleList(DepartmentRolesEnum? selectDepartmenRole = null);
    }
}
