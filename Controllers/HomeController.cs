using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Southwest_Airlines.Models;
using Southwest_Airlines.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Southwest_Airlines.Models.SharedViewModels;
using Southwest_Airlines.Filters;
namespace Southwest_Airlines.Controllers
    //Code by Kenneth Gordon
{
    public class HomeController : Controller
    {

        private readonly DBUserList _DBUserListservice;
        //public HomeController(UserListService userListService)
        //{
        //    _userListService = userListService;

        //}

        public HomeController(DBUserList DBUserListservice)
        {
            _DBUserListservice = DBUserListservice;
        }
        //public async Task<IActionResult> TestConsoleOutput()
        //{
        //    await _DBUserListservice.GetDataAsync();
        //    return Content("Check console for output.");
        //}

        //Display Pages
        [HttpGet]
        public IActionResult info()
        {
            return View();
        }
        public IActionResult registration()
        {
            return View();
        }

        public IActionResult purchase()
        {
            return View();
        }

        [HttpGet]
        public IActionResult payment(int passType)
        {
            ViewBag.PassChoice = new PassChoice { PassType = passType };
            var loginModel = new Login();
            var paymentModel = new Payment();

            var viewModel = new PaymentPageModel
            {
                
                Payment = paymentModel
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(BaseViewModel baseViewModel, string returnUrl)
        {
            var loginInfo = baseViewModel.Login;
            if (ModelState.IsValid)
            {
                bool isValidUser = await _DBUserListservice.VerifyLoginAsync(loginInfo.TBuser, loginInfo.TBpass);
                
                if (isValidUser)
                {
                    string firstName = await _DBUserListservice.GetUserFirstNameAsync(loginInfo.TBuser);
                    //Set up the authentication cookie and sign in the user
                    if (string.IsNullOrEmpty(firstName))
                    {
                        firstName = "Guest"; // Default value if first name is not found
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginInfo.TBuser),
                        new Claim("FirstName", firstName)

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
                Console.WriteLine(registrationInfo.TBlname);
                bool isRegistered = await _DBUserListservice.RegisterUserAsync(registrationInfo);
                if (isRegistered)
                {
                    return RedirectToAction("info");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
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


        [HttpPost]
        public async Task<IActionResult> purchased()
        {
            return RedirectToAction("info");
        }
        [HttpPost]
        [ServiceFilter(typeof(SkipLoginValidationFilter))]
        public async Task<IActionResult> payment(PaymentPageModel paymentPageModel)
            {
            if (ModelState.IsValid)
            {
                var paymentInfo = paymentPageModel.Payment;
                // Process the payment information here
                // For example, save it to the database or perform validation
                Console.WriteLine("Payment processed successfully.");
                Console.WriteLine(paymentInfo.TBcardName);
                return RedirectToAction("info");
            }
            else
            {

                // If the model state is invalid, return the view with validation errors
                ViewBag.PassChoice = new PassChoice { PassType = paymentPageModel.Payment.PassType };
                return View(paymentPageModel);
            }
        }
    }
}
