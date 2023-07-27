using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Departament;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.WebApp.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public DepartmentController(IDepartmentService departmentService, IEmployeeService userService)
        {
            _departmentService = departmentService;
            _employeeService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departamentsList = await _departmentService.GetDepartamentShortInfoListAsync();

            return View(departamentsList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentFullInfoAsync(id);

            return View(department);
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(int? id)
        {      
            var department = new DepartmentEditModel();

            if (id is not null)
            {
                department = await _departmentService.GetDepartmentForEditAsync(id.Value);
                department.UserSelectList = await _employeeService.GetEmployeesSelectListAsync(department.UserIds);
                department.DepartmentSelectList = await _departmentService.GetDepartamentSelectListAsync(department.Id, department.ParentDepartmentId);

                return View(department);
            }

            department.UserSelectList = await _employeeService.GetEmployeesSelectListAsync();
            department.DepartmentSelectList = await _departmentService.GetDepartamentSelectListAsync();

            return View(department); 
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(DepartmentEditModel departmentModel)
        {
            if (!ModelState.IsValid)
            {
                departmentModel.UserSelectList = await _employeeService.GetEmployeesSelectListAsync(departmentModel.UserIds);
                departmentModel.DepartmentSelectList = await _departmentService.GetDepartamentSelectListAsync(departmentModel.Id, departmentModel.ParentDepartmentId);

                return View(departmentModel);
            }

            try
            {
                DepartmentModel? department = null;

                if(departmentModel.Id == 0)
                {
                    department = await _departmentService.AddDepartamentAsync(departmentModel);
                }
                else
                {
                    department = await _departmentService.EditDepartmnentAsync(departmentModel.Id, departmentModel);
                }               

                if (!string.IsNullOrEmpty(departmentModel.ManagerId))
                {
                    await _employeeService.AddManagerToDepartamentAsync(department.Id, departmentModel.ManagerId);
                }

                if (departmentModel.UserIds is not null && departmentModel.UserIds.Any())
                {
                    await _employeeService.AddEmployeeToDepartamentAsync(department.Id, departmentModel.UserIds.ToArray());
                }

                if(departmentModel.ParentDepartmentId is not null)
                {
                    await _departmentService.AddParentDepartmnentAsync(department.Id, departmentModel.ParentDepartmentId.Value);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(departmentModel);
            }

            return RedirectToAction("Details", "Department", new { departmentModel.Id});
        }
    }
}
