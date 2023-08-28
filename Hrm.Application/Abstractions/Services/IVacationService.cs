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
        Task AddVacationRequest(VacationRequesModel vacationRequest);
        Task<VacationFullInfoModel> GetVacationFullInfoAsync(string userId);
    }
}
