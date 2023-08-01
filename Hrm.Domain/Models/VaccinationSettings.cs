using Hrm.Domain.Enums;

namespace Hrm.Domain.Models
{
    public class VaccinationSettings
    {
        public int CounOfDays { get; set; }
        public VaccinationPeriod Period { get; set; }
        public bool NeedAccept { get; set; }
        public bool NegativeBalance { get; set; }
        public bool AllowMoveToNextYear { get; set; }
    }
}
