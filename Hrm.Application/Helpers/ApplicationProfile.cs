using AutoMapper;
using Hrm.Domain.Entities;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Departament;
using Hrm.Domain.ViewModels.Employee;
using System.Xml.Linq;

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
        }
    }
}