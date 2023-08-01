using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.ViewModels.New;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class NewService: INewService
    {
        private readonly IRepository<New> _newRepository;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public NewService(IRepository<New> newRepository, IMapper mapper, IStorageService storageService)
        {
            _newRepository = newRepository;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task AddNewAsync(NewShortInfoModel model, string? creatorId)
        {
            var news = _mapper.Map<New>(model);
            news.CreatorId = creatorId;

            await _newRepository.AddAsync(news);
            await _newRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewShortInfoModel>> GetNewsListAsync()
        {
            return await _newRepository
                .Query()
                .Select(x => _mapper.Map<NewShortInfoModel>(x))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<NewShortInfoModel>> GetNewsListAsync(int take)
        {
            return await _newRepository
                .Query()
                .OrderBy(x => x.CrerateAt)
                .Take(3)
                .Select(x => _mapper.Map<NewShortInfoModel>(x))
                .ToArrayAsync();
        }

        public async Task<NewFullInfoModel?> GetNewsAsync(int id)
        {
            return await _newRepository
                .Query()
                .Include(x => x.Creator)
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<NewFullInfoModel>(x))
                .FirstOrDefaultAsync();
        }       
    }
}
