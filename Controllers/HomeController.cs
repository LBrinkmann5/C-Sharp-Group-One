using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Southwest_Airlines.Models;
using Southwest_Airlines.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace Southwest_Airlines.Controllers
    //Code by Kenneth Gordon
{
    public class HomeController : Controller
    {
        private readonly UserListService _userListService;

        private readonly Test _testservice;
        //public HomeController(UserListService userListService)
        //{
        //    _userListService = userListService;

        //}

        public HomeController(Test testservice)
        {
            _testservice = testservice;
        }

        public async Task<IActionResult> TestConsoleOutput()
        {
            await _testservice.GetDataAsync();
            return Content("Check console for output.");
        }
        [HttpGet]
        public IActionResult info()
        {
            return View();
        }
        public IActionResult registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login loginInfo, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                bool isValidUser = await _testservice.VerifyLoginAsync(loginInfo.TBuser, loginInfo.TBpass);
                if (isValidUser)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginInfo.TBuser),
                        //new Claim("FirstName", user.TBfname)

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Url.Action("info", "Home");
            }
            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> registration(Registration registrationInfo)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _testservice.RegisterUserAsync(registrationInfo);
                if (isRegistered)
                {
                    return RedirectToAction("info");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
                //_userListService.AddUser(registrationInfo);
                //System.Diagnostics.Debug.WriteLine(_userListService.GetUsers().Count);
                return RedirectToAction("info");
            }
            else
            {

                return View(registrationInfo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("info");

        }
    }
}
