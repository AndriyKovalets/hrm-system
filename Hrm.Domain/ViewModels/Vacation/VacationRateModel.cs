using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacationRateModel
    {
        public int Id { get; set; }
        public DateTime CrerateAt { get; set; }
        [Required]
        [DisplayName("Date From")]
        public DateTime DateFrom { get; set; }
        [Required]
        [DisplayName("Date To")]
        public DateTime DateTo { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public int Days { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
