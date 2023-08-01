using Hrm.Domain.ViewModels.New;

namespace Hrm.Domain.ViewModels.Dashboard
{
    public class DashboardModel
    {
        public IEnumerable<NewShortInfoModel>? LastNews { get; set; }
    }
}
