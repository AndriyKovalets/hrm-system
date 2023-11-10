using Hrm.Domain.Abstractions;

namespace Hrm.Domain.Entities
{
    public class VacationRate : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CrerateAt { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Days { get; set; }
        public int Rate { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
