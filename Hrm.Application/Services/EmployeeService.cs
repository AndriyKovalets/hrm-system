using AutoMapper;
using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Hrm.Application.Services
{
    internal class EmployeeService: IEmployeeService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<User> _userRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public EmployeeService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IRepository<User> userRepository,
            IStorageService storageService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task CreateEmployeeAsync(EmployeeModel employee, string roleName, IFormFile? userImage = null, string? password = null)
        {
            var user = _mapper.Map<User>(employee);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if(await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                throw new UserEmailExistExeption();
            }

            if(string.IsNullOrEmpty(user.UserName))
            {
                user.UserName = user.Email;
            }

            if (userImage is not null)
            {
                user.UserImgUrl = await _storageService.AddFileAsync(userImage.OpenReadStream(), userImage.FileName);
            }

            var createUserResult = (password is not null)? await _userManager.CreateAsync(user, password): await _userManager.CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                StringBuilder errors = new StringBuilder();

                foreach (var error in createUserResult.Errors)
                {
                    errors.AppendLine(error.Description);
                }

                throw new UserCreateExeption(errors.ToString());
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task AddEmployeeToDepartamentAsync(int departmentId, params string[] userIds)
        {
            var users = await _userRepository
                .Query()
                .Where(x => userIds.Contains(x.Id))
                .ToArrayAsync();

            foreach(var user in users)
            {
                user.DepartmentId = departmentId;
                user.DepartmentRole = DepartmentRolesEnum.Member;
            }

            await _userRepository.SaveChangesAsync();
        }

        public async Task AddManagerToDepartamentAsync(int departmentId, string? managerId)
        {
            var manager = await _userRepository.GetByKeyAsync(managerId);
            var oldManager = await _userRepository
                .Query()
                .FirstOrDefaultAsync(x => x.DepartmentId == departmentId && x.DepartmentRole == DepartmentRolesEnum.Manager);

            if(oldManager is not null)
            {
                oldManager.DepartmentRole = DepartmentRolesEnum.Member;
            }

            if (manager is null)
            {
                return;
            }

            manager.DepartmentId = departmentId;
            manager.DepartmentRole = DepartmentRolesEnum.Manager;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetEmployeesSelectListAsync(IEnumerable<string>? selectedUsers = null)
        {
            var users = await _userRepository
                .Query()
                .Include(x => x.Department)
                .ToListAsync();

            var userList = users.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = $"{x.GetFullName()} {x.Department?.Name}"
            }).ToList();

            if (selectedUsers is null)
            {
                userList.Insert(0, new SelectListItem { Value = "", Text = "Members", Disabled = true, Selected = true });
            }
            else
            {
                foreach (var user in userList)
                {
                    user.Selected = selectedUsers.Contains(user.Value);
                }
            }

            return userList;
        }

        public async Task<IEnumerable<EmployeeShortInfoModel>> GetEmployeeShortInfoListAsync()
        {
            return await _userRepository
                .Query()
                .Include(x => x.Department)
                .Select(x => _mapper.Map<EmployeeShortInfoModel>(x))
                .ToListAsync();
        }

        public async Task<EmployeeFullInfoModel?> GetEmployeeFullInfoAsync(string id)
        {
            return await _userRepository
                .Query()
                .Include(x => x.Department)
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<EmployeeFullInfoModel>(x))
                .FirstOrDefaultAsync();
        }
    }
}
