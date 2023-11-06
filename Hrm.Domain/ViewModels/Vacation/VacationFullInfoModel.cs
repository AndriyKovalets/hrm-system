namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacationFullInfoModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public double VacationBalance { get; set; }
        public IEnumerable<VacationHistoryModel>? VacationHistories { get; set; }
        public IEnumerable<VacationRateModel>? VacationRates { get; set; }
    }
}
