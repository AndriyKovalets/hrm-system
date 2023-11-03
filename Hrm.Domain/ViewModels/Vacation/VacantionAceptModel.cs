namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacantionAceptModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public VacationHistoryModel? VacationRequest { get; set; }
    }
}
