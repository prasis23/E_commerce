using EmployeeManagement.Data;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        //manages users
        private readonly UserManager<IdentityUser> _userManager;
        //helps in signin process
        private readonly SignInManager<IdentityUser> _signInManager;
        //manages the role
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(ApplicationDbContext applicationDbContext,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterUser()
        {
            //get role details
            ViewBag.RoleList = _applicationDbContext.Roles.ToList();
            return View();
        }
        public async Task<IActionResult> RegisterUsers(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser()
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };
            var userCreateResult = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (userCreateResult.Succeeded)
            {
                var result = await _userManager
               .AddToRoleAsync(user, registerViewModel.RoleName);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("LoginUser");
                }
            }
            else
            {
                //handle error and display on register vier page
                foreach(var item in userCreateResult.Errors)
                {
                    //pass eroor on model to catch on view asp-validation-summary
                    ModelState.AddModelError(string.Empty, "Error!"+item.Description);
                 
                    //get role details
                    ViewBag.RoleList = _applicationDbContext.Roles.ToList();
                    //return to same view 
                    return View("RegisterUser",registerViewModel);

                }
            }
           
            return RedirectToAction("RegisterUser");
        }
        public IActionResult LoginUser()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            //for logout
            await _signInManager.SignOutAsync();

            //retirect to login view after sign out
            return RedirectToAction("LoginUser");
        }
        public async Task<IActionResult> LoginUserDetails(LoginViewModel loginViewModel)
        {
            //passwordsigninasync takes email,password,rememberme,lockoutenabled
            var identiityResult = await _signInManager.
                PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            if (identiityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LoginUser");
        }//give authorization to admin role only
        [Authorize(Roles ="Admin")]
        public IActionResult Roles()
        {
            return View();
        }
        public async Task<IActionResult> AddRoleDetails(RoleViewModel roleViewModel)
        {
            var role = new IdentityRole()
            {
                Name = roleViewModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Roles");
        }
    }
}
