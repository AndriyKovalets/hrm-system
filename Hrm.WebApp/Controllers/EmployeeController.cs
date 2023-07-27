using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.WebApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService userService, IDepartmentService departmentService)
        {
            _employeeService = userService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployeeShortInfoListAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var employee = await _employeeService.GetEmployeeFullInfoAsync(id);

            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Position  = model.Position,
                    Description = model.Description,
                    PhoneNumber = model.PhoneNumber,
                    Skills = model.Skills,
                    DateOfbirth = model.DateOfbirth,
                    StartDate = model.StartDate
                };

                await _employeeService.CreateEmployeeAsync(model, SystemRoles.Member, model.UserImage, model.Password);
            }
            catch (UserEmailExistExeption ex)
            {
                ModelState.AddModelError(nameof(EmployeeModel.Email), ex.Message);
                return View(model);
            }
            catch (UserCreateExeption ex)
            {
                ModelState.AddModelError(nameof(EmployeeModel.Password), ex.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> ChangeEmployeeDepartment(string? id)
        {
            var employee = await _employeeService.GetEmployeeFullInfoAsync(id);

            var model = new EmployeeChangeDepartmentModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                UserImage = employee.UserImgUrl,
                Department = employee.DepartmentName,
                DepartmentId = employee.DepartmentId,
                DepartmentRole = employee.DepartmentRole,
                DepartmentSelectList = await _departmentService.GetDepartamentSelectListAsync(null, employee.DepartmentId),
                DepartmentRoleSelectList = _departmentService.GetDepartamentRoleList(employee.DepartmentRole)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> ChangeEmployeeDepartment(EmployeeChangeDepartmentModel model)
        {
            if (model.DepartmentRole is DepartmentRolesEnum.Manager)
            {
                await _employeeService
                    .AddManagerToDepartamentAsync(model.DepartmentId.Value, model.Id);
            }
            else
            {
                await _employeeService.
                    AddEmployeeToDepartamentAsync(model.DepartmentId.Value, model.Id);
            }

            return RedirectToAction("Details", new { model.Id});
        }
    }
}
