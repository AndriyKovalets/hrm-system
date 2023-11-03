using Hrm.Domain.ViewModels.New;
using Hrm.Domain.ViewModels.Vacation;

namespace Hrm.Domain.ViewModels.Dashboard
{
    public class DashboardModel
    {
        public IEnumerable<NewShortInfoModel>? LastNews { get; set; }
        public IEnumerable<VacantionAceptModel> TodayInVacation {get; set;}
    }
}
