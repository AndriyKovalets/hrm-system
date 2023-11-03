using Hrm.Domain.Enums;

namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacationHistoryModel
    {
        public int Id { get; set; }
        public VacationHistoryType Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Balance { get; set; }
        public string? Description { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
