using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EmployeeManagement.Data
{
    //Used to communicate with sql database and model class
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //Links a model class Employee with database table Employee
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EmployeeShift> EmployeeShift { get; set; }
        //public DbSet<Image> Images { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public DbSet<EmployeeShiftLog> EmployeeShiftLog { get; set; }
        // Fake Entity to call stroed procedure 
        public DbSet<LateInEarlyOutViewModel> Sp_LateInEarlyOut { get; set; }


        //onModelCreating gets call paralley
        //when DB Context get initialized
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EmployeeDepartment>().HasNoKey();
            builder.Entity<LateInEarlyOutViewModel>().HasNoKey();
        }
        
    }
}
