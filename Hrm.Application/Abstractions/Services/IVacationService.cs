using Hrm.Domain.ViewModels.Vacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Abstractions.Services
{
    public interface IVacationService
    {
        Task AcceptVacationAsync(int vacationId, bool isAccept);
        Task AddVacationRequest(VacationRequesModel vacationRequest);
        Task<IEnumerable<VacantionAceptModel>> GetNotAcceptVacationAsync();
        Task<VacationFullInfoModel> GetVacationFullInfoAsync(string userId);
    }
}
