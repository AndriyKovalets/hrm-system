using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.ViewModels.Document;
using Hrm.Domain.ViewModels.New;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class DocumentService: IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IMapper _mapper;

        public DocumentService(IRepository<Document> newRepository, IMapper mapper)
        {
            _documentRepository = newRepository;
            _mapper = mapper;
        }

        public async Task AddDodumentAsync(DocumentShortInfoModel model, string? creatorId)
        {
            var document = _mapper.Map<Document>(model);
            document.CreatorId = creatorId;

            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();
        }

        public async Task EditDocumentAsync(DocumentShortInfoModel model, string? creatorId)
        {
            var document = await _documentRepository.GetByKeyAsync(model.Id);
            document.CreatorId = creatorId;
            document.Header = model.Header;
            document.CrerateAt = DateTime.Now;
            document.Content = model.Content;

            await _documentRepository.UpdateAsync(document);
            await _documentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentFullInfoModel>> GetDodumentListAsync()
        {
            return await _documentRepository
                .Query()
                .Include(x => x.Creator)
                .Select(x => _mapper.Map<DocumentFullInfoModel>(x))
                .ToListAsync();
        }

        public async Task<DocumentFullInfoModel?> GetDodumentAsync(int id)
        {
            return await _documentRepository
                .Query()
                .Include(x => x.Creator)
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<DocumentFullInfoModel>(x))
                .FirstOrDefaultAsync();
        }
    }
}
