using Hrm.Domain.Abstractions;
using Hrm.Domain.Enums;

namespace Hrm.Domain.Entities
{
    public class VacationHistory: IBaseEntity
    {
        public int Id { get; set; }
        public VacationHistoryType Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Balance { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; } 
        public bool? IsAccepted { get; set; }
        public User? User { get; set; }
    }
}
