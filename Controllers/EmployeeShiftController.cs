using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeShiftController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeShiftController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeShift()
        {
            ViewBag.EmployeeList = _applicationDbContext.Employee.
               ToList();
            ViewBag.UserList = _applicationDbContext.Users.ToList();
            return View();
        }
        public IActionResult AddEmployeeShift(EmployeeShift EmployeeShift)
        {
            //Add employee instance in Employee table
            _applicationDbContext.EmployeeShift.Add(EmployeeShift);
            //Save the data
            _applicationDbContext.SaveChanges();
            //Redirect to Add Employee view
            return RedirectToAction("EmployeeShift");
        }

        //get employee shift list
        public IActionResult GetEmployeeShift()
        {
            List<EmployeeShift> employeeShift =
                _applicationDbContext.EmployeeShift.ToList();
            return View(employeeShift);
        }

        public IActionResult GetEmployeeShiftById(int id)
        {
            //get department by id and also include the employees list associated with the employeeshift
            EmployeeShift employeeShift = _applicationDbContext.EmployeeShift.
                Include(x => x.Employee).
                FirstOrDefault(x => x.Id == id);
            ViewBag.EmployeetList = _applicationDbContext.
             EmployeeShift.ToList();
            return View(EmployeeShift);
        }
        public IActionResult UpdateEmployeeShiftById(EmployeeShift employeeShift)
        {
            //update employee by id
            _applicationDbContext.EmployeeShift.Update(employeeShift);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllEmployeeShift");
        }
    }
}