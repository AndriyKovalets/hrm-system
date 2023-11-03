using Hrm.Domain.Roles;

namespace Hrm.Domain.ViewModels.Employee
{
    public class EmployeeFullInfoModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? userRole { get; set; }
        public string? UserImgUrl { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public DateTime StartDate { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Skills { get; set; }
        public DateTime DateOfbirth { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DepartmentRolesEnum DepartmentRole { get; set; }
    }
}
