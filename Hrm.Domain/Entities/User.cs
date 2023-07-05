using Hrm.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Hrm.Domain.Entities
{
    public class User: IdentityUser, IBaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
