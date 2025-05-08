using Microsoft.AspNetCore.Mvc;

namespace Southwest_Airlines.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Dashboard()
        {
            //Work in Progress
            return View();
        }
        public async Task<IActionResult> Login()
        {
            //Work in Progress
            return RedirectToAction("Dashboard");
        }
    }
}
