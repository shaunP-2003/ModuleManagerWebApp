using Microsoft.AspNetCore.Mvc;

namespace PROG_POE.Controllers
{
    public class LogLandingPage : Controller
    {
        public IActionResult Index()
        {
            // Redirect to the Identity login page
            return RedirectToAction("Login", "Pages/Account", new { area = "Identity" });
        }
    }
}
