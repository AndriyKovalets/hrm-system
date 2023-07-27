using Hrm.Domain.Roles;

namespace Hrm.Domain.Models
{
    public class UserDepartament
    {
        public string? UserId { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentRolesEnum Role { get; set; }
    }
}
