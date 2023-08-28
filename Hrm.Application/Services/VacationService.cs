using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Enums;
using Hrm.Domain.Exeptions;
using Hrm.Domain.ViewModels.Vacation;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class VacationService: IVacationService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<VacationHistory> _vacationHistoryRepository;
        private readonly ISettingsService _settinsService;

        public VacationService(
            IMapper mapper,
            IRepository<User> userRepository,
            IRepository<VacationHistory> vacationHistoryRepository,
            ISettingsService settinsService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _vacationHistoryRepository = vacationHistoryRepository;
            _settinsService = settinsService;
        }

        public async Task<VacationFullInfoModel> GetVacationFullInfoAsync(string userId)
        {
            var vacationInfo = await _userRepository
                                .Query()
                                .Include(x => x.VacationHistories)
                                .FirstOrDefaultAsync(x => x.Id == userId);

            return _mapper.Map<VacationFullInfoModel>(vacationInfo);
        }

        public async Task AddVacationRequest(VacationRequesModel vacationRequest)
        {
            var settings = await _settinsService.GetVacationSettingsAsync();

            if(settings is null)
            {
                throw new Exception();
            }

            var user = await _userRepository.GetByKeyAsync(vacationRequest.UserId);

            var vacationHistory = _mapper.Map<VacationHistory>(vacationRequest);

            vacationHistory.Balance = (vacationHistory.DateTo - vacationHistory.DateFrom).Days;
            vacationHistory.Type = VacationHistoryType.Request;

            if(!settings.NegativeBalance && user.VacationBalance - vacationHistory.Balance < 0)
            {
                throw new VacationRequestExeption("You don't have enough vacation balance");
            }
            
            if (settings.NegativeBalance && Math.Abs(user.VacationBalance - vacationHistory.Balance) < settings.MaxNegativeBalance)
            {
                throw new VacationRequestExeption("You don't have enough vacation balance");
            }

            await _vacationHistoryRepository.AddAsync(vacationHistory);
            await _vacationHistoryRepository.SaveChangesAsync();
        }
    }
}
