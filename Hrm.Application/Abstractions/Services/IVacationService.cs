using Hrm.Domain.ViewModels.Vacation;

namespace Hrm.Application.Abstractions.Services
{
    public interface IVacationService
    {
        Task AcceptVacationAsync(int vacationId, bool isAccept);
        Task AddVacationRateAsync(VacationRateModel vacationRate, string userId);
        Task AddVacationRequestAsync(VacationRequesModel vacationRequest);
        Task DeleteVacationRatesAsync(int rateId);
        Task<IEnumerable<VacantionAceptModel>> GetCurrentVacationAsync();
        Task<IEnumerable<VacantionAceptModel>> GetNotAcceptVacationAsync();
        Task<VacationFullInfoModel> GetVacationFullInfoAsync(string userId);
        Task<IEnumerable<VacationPlanModel>> GetVacationPlanAsync();
        Task<IEnumerable<VacationPlanModel>> GetVacationPlanForUserAsync(string userId);
        Task<IEnumerable<VacationRateModel>> GetVacationRatesAsync(string userId);
        Task<IEnumerable<VacantionAceptModel>> TodayInVacationAsync();
    }
}
