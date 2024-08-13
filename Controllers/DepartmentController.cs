using AutoMapper;
using HRManagement.Models;
using HRManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetDepartments();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // Tương tự các action Edit, Delete
    }

}
