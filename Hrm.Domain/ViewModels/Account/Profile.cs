using Hrm.Domain.Roles;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Account
{
    public class Profile
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
        public string? UserImgUrl { get; set; }

        public string? DepartmentName { get; set; }
        public DepartmentRolesEnum DepartmentRole { get; set; }
    }
}
