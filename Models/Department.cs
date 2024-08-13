using System.ComponentModel.DataAnnotations;

namespace HRManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string DepartmentCode { get; set; }

        [Required]
        public string Location { get; set; }

        // If Employees is a navigation property, it should be optional or not included in the create form
        public ICollection<Employee>? Employees { get; set; }
    }
}
