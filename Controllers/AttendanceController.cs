using EmployeeManagement.Data;
using EmployeeManagement.Migrations;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private int employeeShiftId;

        public AttendanceController(ApplicationDbContext applicationDbContext,

            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Attendance()
        {
            //get logged in user
            var logedInUser = await _userManager.GetUserAsync(User);
            ViewBag.CheckInCheck = "Check In";
            ViewBag.AttendanceId = null;
            ViewBag.EmployeeShiftLogs = new List<EmployeeShiftLogViewModel>();
            if (logedInUser != null)
            {
                //get employee from logged in user id
                var employeeDetails = _applicationDbContext.Employee
                    .Where(emp => emp.IdentityUserId ==
                logedInUser.Id).FirstOrDefault();
                if (employeeDetails != null)
                {
                    var employeeShiftlogId=_applicationDbContext.EmployeeShift
                        .Where(x => x.EmployeeId == employeeDetails.Id).Select(x => x.Id)
                        .FirstOrDefault();
                    if (employeeShiftlogId !=null)
                    {
                        ViewBag.EmployeeShiftLogs = _applicationDbContext.EmployeeShiftLog.
                            Where(x => x.EmployeeId == employeeDetails.Id && x.EmployeeShiftId == employeeShiftId)
                            .Select(x=> new EmployeeShiftLogViewModel
                            {
                                EmployeeName = employeeDetails.Name,
                                CheckInTime = x.CheckInTime.HasValue ? x.CheckInTime.Value.ToString("hh:mm:tt") : null,
                                CheckOutTime = x.CheckOutTime.HasValue ? x.CheckOutTime.Value.ToString("hh:mm:tt") : null,
                            }).ToList();
                    }
                    //take last shift data
                    var employeeShiftLog = _applicationDbContext.EmployeeShiftLog
                        .Where(es => es.EmployeeId == employeeDetails.Id
                        && es.CheckInTime.Value.Date == DateTime.Today.Date)
                        .OrderByDescending(es => es.id).FirstOrDefault();
                    if (employeeShiftLog != null)
                    {
                        if (employeeShiftLog.CheckInTime != null &&
                            employeeShiftLog.CheckOutTime == null)
                        {
                            ViewBag.CheckInCheck = "Check Out";
                            ViewBag.AttendanceId = employeeShiftLog.id;
                        }
                    }
                }
            }
            return View();
            }

                public async Task<IActionResult> CheckInEmployee(int? attendanceId)
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (loggedInUser != null)
            {
                //get employee from logged in user id
                   var employeeDetails = _applicationDbContext.Employee
                        .Where(emp => emp.IdentityUserId ==
                loggedInUser.Id).FirstOrDefault();
                if (employeeDetails != null)
            {
                var employeeShiftId = _applicationDbContext.EmployeeShift
                    .Where(x => x.EmployeeId == employeeDetails.Id)
                    .Select(x => x.Id).FirstOrDefault();
                    if (attendanceId == null)
                {
                    EmployeeShiftLog employeeShiftLog = new EmployeeShiftLog();
                    employeeShiftLog.CheckInTime = DateTime.Now;
                    employeeShiftLog.EmployeeId = employeeDetails.Id;
                    employeeShiftLog.EmployeeShiftId = employeeShiftId;
                    _applicationDbContext.EmployeeShiftLog.Add(employeeShiftLog);
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    EmployeeShiftLog? employeeShiftLog = _applicationDbContext.EmployeeShiftLog
                        .Where(es => es.id == attendanceId).FirstOrDefault();
                    if (employeeShiftLog != null)
                    {
                        employeeShiftLog.CheckOutTime = DateTime.Now;
                        _applicationDbContext.EmployeeShiftLog.Update(employeeShiftLog);
                        _applicationDbContext.SaveChanges();
                    }
                }
            }
        }
        return RedirectToAction("Attendance");
    }
    public async Task<IActionResult> LateInEarlyOut()
        {
            var loggedInUser=await _userManager.GetUserAsync(User);
            ViewBag.EmployeeShiftlogs = new List<LateInEarlyOutViewModel>();
            if (loggedInUser != null) 
            {
                var employeeDetails = _applicationDbContext.Employee
                    .Where(x => x.IdentityUserId ==
                    loggedInUser.Id).FirstOrDefault();
                if(employeeDetails!=null){
                    int param=new SqlParameter("@EmployeeId",employeeDetails.Id);
                    //call the store procedures
                    var result = _applicationDbContext.Sp_LateInEarlyOut
                        .FromSqlRaw("[dbo].[Sql_lateInEarlyOut]", param).ToList();
                    ViewBag.EployeeShiftLogs = result;

                }
            }
            return View();

        }
}
}
