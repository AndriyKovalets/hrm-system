namespace Hrm.Domain.ViewModels.Vacation
{
    public class VacationPlanModel
    {
        public int Id { get; set; }
        public DateTime CrerateAt { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }
}

