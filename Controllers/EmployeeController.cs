using AutoMapper;
using HRManagement.Models;
using HRManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetEmployees();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _departmentRepository.GetDepartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _departmentRepository.GetDepartments();
            return View(employee);
        }

        // Tương tự các action Edit, Delete
    }

}
