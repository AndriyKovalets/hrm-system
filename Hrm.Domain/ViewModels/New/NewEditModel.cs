using Microsoft.AspNetCore.Http;

namespace Hrm.Domain.ViewModels.New
{
    public class NewEditModel: NewShortInfoModel
    {
        public IEnumerable<IFormFile>? Images { get; set; }
    }
}
