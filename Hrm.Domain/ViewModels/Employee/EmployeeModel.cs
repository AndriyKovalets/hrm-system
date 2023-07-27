using Hrm.Domain.Roles;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Employee
{
    public class EmployeeModel
    {
        [Required]
        [DisplayName("First name")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string? LastName { get; set; }

        [Required]
        [DisplayName("Position")]
        public string? Position { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("Phone")]
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? Skills { get; set; }

        [DisplayName("Date of birth")]
        public DateTime DateOfbirth { get; set; }

        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [DisplayName("User image")]
        public IFormFile? UserImage { get; set; }

        [Required]
        [DisplayName("Password")]
        public string? Password { get; set; }
        public int? DepartmentId { get; set; }
        public DepartmentRolesEnum DepartmentRole { get; set; }
    }
}
