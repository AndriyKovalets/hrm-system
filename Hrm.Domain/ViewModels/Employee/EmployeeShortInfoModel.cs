using Hrm.Domain.Roles;

namespace Hrm.Domain.ViewModels.Employee
{
    public class EmployeeShortInfoModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? UserImgUrl { get; set; }
        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public DepartmentRolesEnum DepartmentRole { get; set; }
    }
}
