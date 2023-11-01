using Hrm.Domain.Abstractions;

namespace Hrm.Domain.Entities
{
    public class VacationPlan : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CrerateAt { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
