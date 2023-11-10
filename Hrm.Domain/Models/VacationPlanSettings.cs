namespace Hrm.Domain.Models
{
    public class VacationPlanSettings
    {
        public int N { get; set; } = 50;
        public int Max { get; set; } = 30;
        public int TMax { get; set; } = 1500;
        public int StepA { get; set; } = 1;
        public int StepB { get; set; } = 1;
    }
}
