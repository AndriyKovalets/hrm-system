using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;

namespace Hrm.Application.Services
{
    internal class NewService: INewService
    {
        private readonly IRepository<New> _newRepository;

        public NewService(IRepository<New> newRepository)
        {
            _newRepository = newRepository;
        }
    }
}
