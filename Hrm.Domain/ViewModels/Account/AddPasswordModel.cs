using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Account
{
    public class AddPasswordModel
    {
        [Required]
        public string? Password { get; set; }
    }
}
