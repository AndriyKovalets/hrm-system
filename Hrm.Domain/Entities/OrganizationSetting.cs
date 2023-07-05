using Hrm.Domain.Abstractions;

namespace Hrm.Domain.Entities
{
    public class OrganizationSetting: IBaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Settings { get; set; }
    }
}
