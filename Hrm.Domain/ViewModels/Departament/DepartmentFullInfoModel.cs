namespace Hrm.Domain.ViewModels.Departament
{
    public class DepartmentFullInfoModel: DepartmentModel
    {
        public string? ParentDepartmentName { get; set; }
        public string? ManagerName  { get; set; }
        public Dictionary<string, string>? Users { get; set; }
    }
}
