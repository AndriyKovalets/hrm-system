using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacationRequesModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Datre from")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DisplayName("Date to")]
        public DateTime DateTo { get; set; }
        public string UserId { get; set; }
    }
}
