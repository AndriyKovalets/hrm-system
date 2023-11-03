using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Departament;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<DepartmentModel> AddDepartamentAsync(DepartmentEditModel departmentEditModel)
        {
            var department = _mapper.Map<Department>(departmentEditModel);

            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();

            return _mapper.Map<DepartmentModel>(department);
        }

        public async Task<IEnumerable<DepartmentShortInfoModel>> GetDepartamentShortInfoListAsync()
        {
            return await _departmentRepository.
                Query()
                .Include(x => x.ParentDepartment)
                .Include(x=> x.Users)
                .Select(x => new DepartmentShortInfoModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentDepartment = x.ParentDepartment.Name,
                    ParentDepartmentId = x.ParentDepartment.Id,
                    Manager = x.Users != null ? x.Users.FirstOrDefault(y => y.DepartmentRole == DepartmentRolesEnum.Manager) : null,
                    CountOfMemebers = x.Users != null ? x.Users.Count() : 0

                }).ToListAsync();
        }

        public async Task<DepartmentModel> EditDepartmnentAsync(int departmentId, DepartmentEditModel departmentEditMpdel)
        {
            var department = await _departmentRepository.GetByKeyAsync(departmentId);
            
            department.Name = departmentEditMpdel.Name;
            department.Description = departmentEditMpdel.Description;
            department.MinEmployeeNeed = departmentEditMpdel.MinEmployeeNeed;

            await _departmentRepository.UpdateAsync(department);
            await _departmentRepository.SaveChangesAsync();

            return _mapper.Map<DepartmentModel>(department);
        }

        public async Task AddParentDepartmnentAsync(int departmentId, int parentDepartmentId)
        {
            var department = await _departmentRepository.GetByKeyAsync(departmentId);
            department.ParentDepartamentId = parentDepartmentId;

            await _departmentRepository.UpdateAsync(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task<DepartmentFullInfoModel> GetDepartmentFullInfoAsync(int departmentId)
        {
            var department = await _departmentRepository.
                Query()
                .Include(x => x.ParentDepartment)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == departmentId);

            return _mapper.Map<DepartmentFullInfoModel>(department);
        }

        public async Task<DepartmentEditModel> GetDepartmentForEditAsync(int departmentId)
        {
            var department = await _departmentRepository.
                Query()
                .Include(x => x.ParentDepartment)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == departmentId);

            var departmentToReturn = new DepartmentEditModel()
            {
                Id = department.Id,
                Name = department?.Name,
                Description = department?.Description,
                DateCreate = department.DateCreate,
                MinEmployeeNeed = department.MinEmployeeNeed,
                ManagerId = department?.Users?.FirstOrDefault(x => x.DepartmentRole == DepartmentRolesEnum.Manager)?.Id,
                ParentDepartmentId = department?.ParentDepartamentId,
                UserIds = department?.Users?.Where(x => x.DepartmentRole != DepartmentRolesEnum.Manager)?.Select(x => x.Id)
            };

            return departmentToReturn;
        }

        public async Task<IEnumerable<SelectListItem>> GetDepartamentSelectListAsync(int? excludeDepartmentId = null, int? selectDepartmentId = null)
        {
            var departments = await _departmentRepository.GetAllAsync(); ;

            var departmentList = new List<SelectListItem>();

            departmentList.AddRange(departments.Where(x => x.Id != excludeDepartmentId)
           .Select(x => new SelectListItem
           {
               Value = x.Id.ToString(),
               Text = x.Name
           }));

            if (!selectDepartmentId.HasValue)
            {
                departmentList.Insert(0, new SelectListItem { Text = "Departments", Value = "", Disabled = true, Selected = true });
            }
            else
            {
                departmentList.First(x => x.Value == selectDepartmentId.ToString()).Selected = true;
            }

            return departmentList;
        }

        public IEnumerable<SelectListItem> GetDepartamentRoleList(DepartmentRolesEnum? selectDepartmenRole = null)
        {
            var enumValues = Enum.GetValues(typeof(DepartmentRolesEnum)).Cast<DepartmentRolesEnum>();
            var selectListItems = enumValues.Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)Convert.ChangeType(v, typeof(int))).ToString(),
                Disabled = false,
                Selected = false
            }).ToList();


            selectListItems.Insert(0, new SelectListItem
            {
                Text = "Roles",
                Value = "",
                Disabled = true,
                Selected = false
            });

            if (selectDepartmenRole is not null)
            {
                var item = selectListItems.FirstOrDefault(x => x.Text == selectDepartmenRole.Value.ToString());

                item.Selected = true;
            }

            return selectListItems;
        }
    }
}
