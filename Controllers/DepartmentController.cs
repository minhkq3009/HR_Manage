using AutoMapper;
using HRManagement.Models;
using HRManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using HRManagement.Data;
using Microsoft.EntityFrameworkCore;


namespace HRManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly HRDbContext _context;

        public DepartmentController(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch all departments from the database
            var departments = await _context.Departments
                .Select(d => new
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    DepartmentCode = d.DepartmentCode,
                    Location = d.Location
                })
                .ToListAsync();

            // Pass the data to the view
            return View(departments);
        }

        // Show the create department form
        public IActionResult Create()
        {
            return View();
        }

        // Handle the create department form submission
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    // Throw an exception with the first error message
                    var firstError = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage).FirstOrDefault();
                    throw new Exception(firstError ?? "Validation error occurred.");
                }

                // Add the department to the database
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // Return a BadRequest result with the error message
                return BadRequest(e.Message);
            }
        }

        // Show the edit department form
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // Handle the edit department form submission
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // Handle department deletion
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // Confirm department deletion
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                Console.WriteLine("Department removed successfully.");
            }
            else
            {
                Console.WriteLine("Department not found.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }

}
