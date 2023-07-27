using Hrm.Domain.Abstractions;

namespace Hrm.Domain.Entities
{
    public class New: IBaseEntity
    {
        public int Id { get; set; }
        public string? Header { get; set; }
        public DateTime CrerateAt { get; set; }
        public string? Content { get; set; }
        public string? Images { get; set; }
        public string? CreatorId { get; set; }
        public User? Creator { get; set; }
    }
}
