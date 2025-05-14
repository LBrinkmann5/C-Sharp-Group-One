using Microsoft.AspNetCore.Mvc;

namespace Southwest_Airlines.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult BookFlight()
        {
            return View();
        }
    }
}
