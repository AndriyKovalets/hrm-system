using Hrm.Domain.Roles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Employee
{
    public class EmployeeChangeDepartmentModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? UserImage { get; set; }
        public string? Department { get; set; }

        [Required]
        [DisplayName("Departmnet")]
        public int? DepartmentId { get; set; }

        [Required]
        [DisplayName("Department Role")]
        public DepartmentRolesEnum DepartmentRole { get; set; }
        public IEnumerable<SelectListItem>? DepartmentSelectList { get; set; }
        public IEnumerable<SelectListItem>? DepartmentRoleSelectList { get; set; }
    }
}
