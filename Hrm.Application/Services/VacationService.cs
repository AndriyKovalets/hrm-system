using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Enums;
using Hrm.Domain.Exeptions;
using Hrm.Domain.ViewModels.Vacation;
using Microsoft.EntityFrameworkCore;
using System;

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

            if(vacationInfo.VacationHistories is not null)
                vacationInfo.VacationHistories = vacationInfo?.VacationHistories.OrderByDescending(x => x.Id);

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

            vacationHistory.Balance = (vacationHistory.DateTo - vacationHistory.DateFrom).Days + 1;
            vacationHistory.Type = VacationHistoryType.Request;

            if(!settings.NegativeBalance && user.VacationBalance - vacationHistory.Balance < 0)
            {
                throw new VacationRequestExeption("You don't have enough vacation balance");
            }
            
            if (settings.NegativeBalance && Math.Abs(user.VacationBalance - vacationHistory.Balance) < settings.MaxNegativeBalance)
            {
                throw new VacationRequestExeption("You don't have enough vacation balance");
            }

            vacationHistory.IsAccepted = null;
            user.VacationBalance -= vacationHistory.Balance;

            await _vacationHistoryRepository.AddAsync(vacationHistory);
            await _userRepository.UpdateAsync(user);
            await _vacationHistoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VacantionAceptModel>> GetNotAcceptVacationAsync()
        {
            var notAcceptVacationList = await _vacationHistoryRepository
                .Query()
                .Include(x => x.User)
                .Where(x => x.Type == VacationHistoryType.Request && x.IsAccepted == null)
                .Select(x => new VacantionAceptModel()
                {
                    UserId = x.UserId,
                    UserName = x.User.GetFullNameWithPosition(),
                    VacationRequest = _mapper.Map<VacationHistoryModel>(x)
                })
                .ToListAsync();

            return notAcceptVacationList;
        }

        public async Task AcceptVacationAsync(int vacationId, bool isAccept)
        {
            var vacation = await _vacationHistoryRepository
                .Query()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == vacationId);

            vacation.IsAccepted = isAccept;

            if (!isAccept)
            {
                vacation.User.VacationBalance += vacation.Balance;           
            }

            await _vacationHistoryRepository.UpdateAsync(vacation);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<VacantionAceptModel>> GetCurrentVacationAsync()
        {
            var dateStart = DateTime.Now.AddDays(-7).Date;
            var dateEnd = DateTime.Now.AddDays(28).Date;

            var currentVacationList = await _vacationHistoryRepository
                .Query()
                .Include(x => x.User)
                .Where(x => x.Type == VacationHistoryType.Request && x.IsAccepted != false && 
                (x.DateFrom.Date >= dateStart && x.DateFrom.Date <= dateEnd))
                .OrderBy(x => x.DateFrom)
                .Select(x => new VacantionAceptModel()
                {
                    UserId = x.UserId,
                    UserName = x.User.GetFullNameWithPosition(),
                    VacationRequest = _mapper.Map<VacationHistoryModel>(x)
                })
                .ToListAsync();
            
            return currentVacationList;
        }

        public async Task<IEnumerable<VacantionAceptModel>> TodayInVacationAsync()
        {
            var today = DateTime.Now.Date;

            var currentVacationList = await _vacationHistoryRepository
                .Query()
                .Include(x => x.User)
                .Where(x => x.Type == VacationHistoryType.Request && x.IsAccepted == true &&
                    x.DateFrom.Date >= today && x.DateTo.Date <= today)
                .OrderBy(x => x.DateFrom)
                .Select(x => new VacantionAceptModel()
                {
                    UserId = x.UserId,
                    UserName = x.User.GetFullNameWithPosition(),
                    VacationRequest = _mapper.Map<VacationHistoryModel>(x)
                })
                .ToListAsync();

            return currentVacationList;
        }
    }
}
