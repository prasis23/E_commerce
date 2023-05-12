using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DepartmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult AddDepartment()
        {
            return View();
        }
        public IActionResult AddDepartmentDetails(Department department)
        {
            //Add employee instance in Employee table
            _applicationDbContext.Department.Add(department);
            //Save the data
            _applicationDbContext.SaveChanges();
            //Redirect to Add Employee view
            return RedirectToAction("AddDepartment");
        }
        public IActionResult GetDepartment()
        {
            List<Department> departments =
                _applicationDbContext.Department.ToList();
            return View(departments);
        }
        public IActionResult GetDepartmentById(int id)
        {
            //get department by id and also include the employees list associated with the department
            Department department = _applicationDbContext.Department.Include(x=>x.Employees).
                FirstOrDefault(x => x.Id == id);
            return View(department);
        }
    }
}
