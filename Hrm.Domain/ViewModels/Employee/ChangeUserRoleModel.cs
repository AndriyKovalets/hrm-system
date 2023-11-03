using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Hrm.Domain.ViewModels.Employee
{
    public class ChangeUserRoleModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        [DisplayName("User role")]
        public string? UserRole { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
