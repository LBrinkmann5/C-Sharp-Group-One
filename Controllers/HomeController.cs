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
using Org.BouncyCastle.Bcpg;
using static Southwest_Airlines.Models.User.Airports;
namespace Southwest_Airlines.Controllers
    //Code by Kenneth Gordon
{
    public class HomeController : Controller
    {

        private readonly DBCustomer _DBCustomerService;
        //public HomeController(UserListService userListService)
        //{
        //    _userListService = userListService;

        //}

        public HomeController(DBCustomer DBCustomerService)
        {
            _DBCustomerService = DBCustomerService;
            
        }
        //public async Task<IActionResult> TestConsoleOutput()
        //{
        //    await _DBCustomerService.GetDataAsync();
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
        public IActionResult confirmPurchase()
        {
            ViewBag.PassType = TempData["PassType"];
            ViewBag.Price = TempData["Price"];
            return View();
        }

        public IActionResult flights()
        {
            return View();
        }

        public IActionResult customerDash()
        {
            return View();
        }

        public IActionResult airports()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchAirports(string term)
        {
            var airports = await _DBCustomerService.SearchAirportsAsync(term);
            return Json(airports);
        }

        [HttpGet]
        public async Task<IActionResult> bookingPayment(int flightId, double price, bool useFastPass)
        {
            ViewBag.Flight = flightId;
            ViewBag.Price = price;
            ViewBag.PassengerCount = TempData["Passengers"];
            ViewBag.UseFastPass = useFastPass;
            var bookingPaymentModel = new BookingPaymentModel
            {
                BookingPayment = new BookingPayment(),
                Passengers = new Passengers(),
                fastPasses = new List<FastPassPurchase>(), // Initialize the list
            };

            if (useFastPass && User.Identity.IsAuthenticated)
            {
                var userId = await _DBCustomerService.GetUserIdAsync(User.Identity.Name);
                var fastPasses = await _DBCustomerService.GetUserPurchasesAsync(userId);
                ViewBag.FastPasses = fastPasses;
            }

            return View(bookingPaymentModel);
        }

        public async Task<IActionResult> purchasedPasses()
        {
            
            var username = User.Identity.Name;
            int userID = await _DBCustomerService.GetUserIdAsync(username);
            List<FastPassPurchase> passes = await _DBCustomerService.GetUserPurchasesAsync(userID);
            return View(passes);
        }

        [HttpGet]
        public IActionResult payment(int passType)
        {
            if (User.Identity.IsAuthenticated)
            {

            ViewBag.PassChoice = new PassChoice { PassType = passType };
            var loginModel = new Login();
            var paymentModel = new Payment();

            var viewModel = new PaymentPageModel
            {
                Payment = paymentModel,
            };
            return View(viewModel);
            }
            else
            {
                // If the user is not authenticated, redirect to the login page
                ModelState.AddModelError("", "You must be logged in to make a purchase.");
                return RedirectToAction("purchase");
            }
        }

        public IActionResult booking()
        {
            return View();
        }

        public IActionResult confirmRegistration()
        {
            return View();
        }
        //Cookie authenitcation and login.
        [HttpPost]
        public async Task<IActionResult> Login(BaseViewModel baseViewModel, string returnUrl)
        {
            var loginInfo = baseViewModel.Login;
            if (ModelState.IsValid)
            {
                bool isValidUser = await _DBCustomerService.VerifyLoginAsync(loginInfo.TBuser, loginInfo.TBpass);
                ViewBag.ErrorMessage = ""; // Reset error message
                if (isValidUser)
                {
                    string firstName = await _DBCustomerService.GetUserFirstNameAsync(loginInfo.TBuser);
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
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                    };
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
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
                bool isRegistered = await _DBCustomerService.RegisterUserAsync(registrationInfo);
                if (isRegistered)
                {
                    return RedirectToAction("confirmRegistration");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                    return View(registrationInfo);
                }
                
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
                var userName = User.Identity.Name;
                var userID = await _DBCustomerService.GetUserIdAsync(userName);
                var purchased = new FastPassPurchase();

                TempData["Price"] = paymentInfo.Price.ToString();
                if (paymentInfo.PassType == 1)
                {

                    TempData["PassType"] = "Individual Fast Pass";
                    purchased = new FastPassPurchase
                    {
                        PurchaseId = 0,
                        UserId  = userID,
                        Price = paymentInfo.Price,
                        PassType = paymentInfo.PassType,
                        Passengers = paymentInfo.SelpassNum,
                        PaymentMethod = "Credit Card",
                        PurchaseDate = DateTime.Now,
                    };
                    //More processing of the payment information can be done here
                }
                else if (paymentInfo.PassType == 2)
                {
                    TempData["PassType"] = "Group Fast Pass";
                    purchased = new FastPassPurchase
                    {
                        PurchaseId = 0,
                        UserId = userID,
                        Price = paymentInfo.Price,
                        PassType = paymentInfo.PassType,
                        Passengers = paymentInfo.SelpassNum,
                        PaymentMethod = "Credit Card",
                        PurchaseDate = DateTime.Now,
                    };
                    //More processing of the payment information can be done here
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong. Please try again.");
                    return(RedirectToAction("purchase"));
                }
                // Save the purchase to the database
                await _DBCustomerService.PurchasePassAsync(purchased);
                return RedirectToAction("confirmPurchase");
            }
            else
            {

                // If the model state is invalid, return the view with validation errors
                ViewBag.PassChoice = new PassChoice { PassType = paymentPageModel.Payment.PassType };
                

                return View(paymentPageModel);
            }

        }
        [HttpPost]
        [ServiceFilter(typeof(SkipLoginValidationFilter))]
        public async Task<IActionResult> flights(BookingPageModel bookingPageModel)
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Login") || k.Contains(".Login") || k.StartsWith("Login.") || k.EndsWith(".Login")).ToList())
            {
                ModelState.Remove(key);
            }
            if (ModelState.IsValid)
            {
                var bookingInfo = bookingPageModel.Booking;
                TempData["Passengers"] = bookingInfo.PassengerCount;

                List<Flights> curFlights = await _DBCustomerService.GetFlightsAsync(bookingInfo.DepartDate, bookingInfo.DepartCode, bookingInfo.ArriveCode);
                curFlights = await _DBCustomerService.GetPassAvailabilityAsync(curFlights);
                if (curFlights == null)
                {
                    ModelState.AddModelError("", "No flights found for the selected date and route.");
                    return View("airports",bookingPageModel);
                }
                else
                {  
                    bookingPageModel.FlightsList = curFlights;
                    ViewBag.PassengerCount = TempData["Passengers"];
                    TempData.Keep("Passengers");

                    return View(bookingPageModel);
                }
            }
            else
            {
                // If the model state is invalid, return the view with validation errors
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View("airports", bookingPageModel);
            }
        }
        [HttpPost]
        [ServiceFilter(typeof(SkipLoginValidationFilter))]
        public async Task<IActionResult> bookingPayment(BookingPaymentModel bookingPaymentModel)
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Login") || k.Contains(".Login") || k.StartsWith("Login.") || k.EndsWith(".Login")).ToList())
            {
                ModelState.Remove(key);
            }
            if (ModelState.IsValid)
            {
                var bookingPaymentInfo = bookingPaymentModel.BookingPayment;
                var passengerInfo = bookingPaymentModel.Passengers;
                var flightId = bookingPaymentModel.FlightId;
                var price = bookingPaymentModel.Price;
                var useFastPass = bookingPaymentModel.UseFastPass;
                if (useFastPass)
                {
                    var selectedFastPassId = bookingPaymentModel.SelectedFastPassId;
                    if (selectedFastPassId == null)
                    {
                        ModelState.AddModelError("", "Please select a Fast Pass.");
                        return View(bookingPaymentModel);
                    }
                    else
                    {
                        _DBCustomerService.UsePassAsync(flightId, selectedFastPassId.Value);
                    }

                    // Process the selected Fast Pass ID here
                    // For example, save it to the database or perform any necessary actions
                }
                
                // Process the booking payment information here
                // Redirect to a confirmation page or another action
                return RedirectToAction("confirmPurchase");
            }
            else
            {
                // If the model state is invalid, return the view with validation errors
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(bookingPaymentModel);
            }
        }
    }
}
