using HRManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ReportController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetEmployees();
            var groupedByDepartment = employees
                .GroupBy(e => e.Department.DepartmentName)
                .Select(g => new {
                    DepartmentName = g.Key,
                    EmployeeCount = g.Count()
                }).ToList();

            return View(groupedByDepartment);
        }
    }

}
