using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Departament
{
    public class DepartmentModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [DisplayName("Parent Department")]
        public int? ParentDepartmentId { get; set; }

        public DateTime DateCreate { get; set; }

        [Required]
        [DisplayName("Manager")]
        public string? ManagerId { get; set; }

    }
}
