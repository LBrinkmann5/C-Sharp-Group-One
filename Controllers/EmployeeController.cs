using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Southwest_Airlines.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Southwest_Airlines.Models.SharedViewModels;
using Southwest_Airlines.Filters;
using Humanizer;
using Southwest_Airlines.Models.User;

namespace Southwest_Airlines.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DBEmployee _DBEmployeeService;
        public EmployeeController(DBEmployee DBEmployeeService)
        {
            _DBEmployeeService = DBEmployeeService;
            

        }
        public IActionResult Dashboard()
        {
            //Work in Progress
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(BaseViewModel baseView)
        {
            var loginInfo = baseView.Login;
            if (ModelState.IsValid)
            {
                bool isValidEmployee = await _DBEmployeeService.VerifyLoginAsync(loginInfo.TBuser, loginInfo.TBpass);
                if (isValidEmployee)
                {
                    // Set authentication cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginInfo.TBuser)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal, authProperties);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                
            }
            return RedirectToAction("info", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Response.Cookies.Delete(CookieAuthenticationDefaults.CookieName);
            return RedirectToAction("info", "Home");
        }

        public async Task<IActionResult> fastPassLookup()
        {
            //Work in Progress
            return View();
        }

        public async Task<IActionResult> LogoutOnClose()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Console.WriteLine("This was hit");
            return Ok();
        }
    }
}
