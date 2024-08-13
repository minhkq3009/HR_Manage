using HRManagement.Data;
using HRManagement.Models;
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
    private readonly HRDbContext _context;

    public EmployeeController(HRDbContext context)
    {
        _context = context;
    }

    // GET: Employee/Create
    public IActionResult Create(int departmentId)
    {

        var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
        var employee = new Employee
        {
            DepartmentId = departmentId,
            Department = department
        };

        return View(employee);
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Department");
        }
        return View(employee);
    }
}
