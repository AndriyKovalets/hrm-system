using Hrm.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.Models.Setup
{
    public class SetupOrganizationModel
    {
        [Required]
        [DisplayName("Connection string")]
        public string? ConnectionString { get; set; }

        [Required]
        [DisplayName("Organization name")]
        public string? OrganizationName { get; set; }

        [Required]
        [DisplayName("First name")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string? Password { get; set; }
    }
}
