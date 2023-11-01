using Hrm.Domain.ViewModels.New;

namespace Hrm.Application.Abstractions.Services
{
    public interface INewService
    {
        Task AddNewAsync(NewShortInfoModel model, string? creatorId);
        Task<IEnumerable<NewShortInfoModel>> GetNewsListAsync();
        Task<NewFullInfoModel?> GetNewsAsync(int id);
        Task<IEnumerable<NewShortInfoModel>> GetNewsListAsync(int take);
        Task EditNewAsync(NewShortInfoModel model, string? creatorId);
    }
}
