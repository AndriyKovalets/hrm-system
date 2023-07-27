using Hrm.Domain.Entities;

namespace Hrm.Domain.ViewModels.Departament
{
    public class DepartmentShortInfoModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public User? Manager { get; set; }
        public string? ParentDepartment { get; set; }
        public int? ParentDepartmentId { get; set; }
        public int? CountOfMemebers { get; set; }
    }
}
