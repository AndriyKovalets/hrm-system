using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.Models.Account
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string? Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
