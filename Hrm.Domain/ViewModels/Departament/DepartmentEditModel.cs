using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrm.Domain.ViewModels.Departament
{
    public class DepartmentEditModel: DepartmentModel
    {
        public IEnumerable<string>? UserIds { get; set; }
        public IEnumerable<SelectListItem>? UserSelectList { get; set; }
        public IEnumerable<SelectListItem>? DepartmentSelectList { get; set; }
    }
}
