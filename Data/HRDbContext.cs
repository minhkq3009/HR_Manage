using Microsoft.EntityFrameworkCore;
using HRManagement.Models;

namespace HRManagement.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)        // Each Employee has one Department
                .WithMany(d => d.Employees)       // Each Department has many Employees
                .HasForeignKey(e => e.DepartmentId); // Foreign key in Employee table
        }

    }
}