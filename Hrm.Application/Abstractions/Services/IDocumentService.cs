using Hrm.Domain.ViewModels.Document;

namespace Hrm.Application.Abstractions.Services
{
    public interface IDocumentService
    {
        Task AddDodumentAsync(DocumentShortInfoModel model, string? creatorId);
        Task EditDocumentAsync(DocumentShortInfoModel model, string? creatorId);
        Task<DocumentFullInfoModel?> GetDodumentAsync(int id);
        Task<IEnumerable<DocumentFullInfoModel>> GetDodumentListAsync();
    }
}
