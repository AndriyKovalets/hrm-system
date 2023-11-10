using AutoMapper;
using Hrm.Domain.Entities;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Departament;
using Hrm.Domain.ViewModels.Document;
using Hrm.Domain.ViewModels.Employee;
using Hrm.Domain.ViewModels.New;
using Hrm.Domain.ViewModels.Vacation;

namespace Hrm.Application.Helpers
{
    internal class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<DepartmentModel, Department>();
            CreateMap<DepartmentEditModel, Department>();
            CreateMap<Department, DepartmentModel>();
            CreateMap<Department, DepartmentFullInfoModel>()
                .ForMember(dest => dest.ManagerName, act => act.MapFrom(src => src.Users
                    .FirstOrDefault(x => x.DepartmentRole == DepartmentRolesEnum.Manager).GetFullNameWithPosition()))
                .ForMember(dest => dest.ManagerId, act => act.MapFrom(src => src.Users
                    .FirstOrDefault(x => x.DepartmentRole == DepartmentRolesEnum.Manager).Id))
                .ForMember(dest => dest.ParentDepartmentName, act => act.MapFrom(src => src.ParentDepartment.Name))
                .ForMember(dest => dest.ParentDepartmentId, act => act.MapFrom(src => src.ParentDepartment.Id))
                .ForMember(dest => dest.Users, act => act.MapFrom(src => src.Users
                    .Where(x => x.DepartmentRole != DepartmentRolesEnum.Manager).OrderBy(x => x.Id).ToDictionary(x => x.Id, y => y.GetFullNameWithPosition())));

            CreateMap<EmployeeModel, User>();
            CreateMap<User, EmployeeShortInfoModel>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.GetFullName()))
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, act => act.MapFrom(src => src.Department.Id));
            CreateMap<User, EmployeeFullInfoModel>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.GetFullName()))
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, act => act.MapFrom(src => src.Department.Id));

            CreateMap<User, Domain.ViewModels.Account.Profile>();

            CreateMap<User, VacationFullInfoModel>()
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.GetFullNameWithPosition()));

            CreateMap<VacationHistory, VacationHistoryModel>();
                
            CreateMap<VacationHistoryModel, VacationHistory>();

            CreateMap<NewShortInfoModel, New>()
                .ForMember(dest => dest.CrerateAt, act => act.MapFrom(src => DateTime.Now));

            CreateMap<New, NewShortInfoModel>();

            CreateMap<New, NewFullInfoModel>()
                .ForMember(dest => dest.CreatorName, act => act.MapFrom(src => src.Creator.GetFullName()))
                .ForMember(dest => dest.CreatorId, act => act.MapFrom(src => src.CreatorId));

            CreateMap<VacationRequesModel, VacationHistory>();

            CreateMap<DocumentShortInfoModel, Document>()
                .ForMember(dest => dest.CrerateAt, act => act.MapFrom(src => DateTime.Now));

            CreateMap<Document, DocumentShortInfoModel>();

            CreateMap<Document, DocumentFullInfoModel>()
                .ForMember(dest => dest.CreatorName, act => act.MapFrom(src => src.Creator.GetFullName()))
                .ForMember(dest => dest.CreatorId, act => act.MapFrom(src => src.CreatorId));

            CreateMap<VacationRate, VacationRateModel>()
                .ForMember(dest => dest.CrerateAt, act => act.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.GetFullNameWithPosition()));

            CreateMap<VacationRateModel, VacationRate>();

            CreateMap<VacationPlan, VacationPlanModel>()
               .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.GetFullNameWithPosition()));
        }
    }
}

