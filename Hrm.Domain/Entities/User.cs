using Hrm.Domain.Abstractions;
using Hrm.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace Hrm.Domain.Entities
{
    public class User: IdentityUser, IBaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserImgUrl { get; set; }
        public string? Position { get; set; }
        public DateTime StartDate { get; set; }
        public string? Description { get; set; }
        public string? Skills { get; set; }
        public DateTime DateOfbirth { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public DepartmentRolesEnum DepartmentRole { get; set; }
        public IEnumerable<New> News { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetFullNameWithPosition()
        {
            return $"{FirstName} {LastName} {Position}";
        }
    }
}
