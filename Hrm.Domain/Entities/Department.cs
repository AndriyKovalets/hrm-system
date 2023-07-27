using Hrm.Domain.Abstractions;

namespace Hrm.Domain.Entities
{
    public class Department: IBaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreate { get; set; }
        public int? ParentDepartamentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Department>? SubDepartments { get; set; }
    }
}
