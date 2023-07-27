using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Account
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
