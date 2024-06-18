using Microsoft.AspNetCore.Mvc;
using PROG_POE.Data;
using PROG_POE.Models.Domain;
using System.Security.Claims;

namespace PROG_POE.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ModuleAppDbContext moduleAppDbContext;

        public ModuleController(ModuleAppDbContext moduleAppDbContext)
        {
            this.moduleAppDbContext = moduleAppDbContext;
        }

        public IActionResult AddModule()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddModule(ModuleViewM moduleRequest)
        {
            

            var mod = new Modules()
            {

                ModuleId = Guid.NewGuid(),
                MCode= moduleRequest.MCode,
                MName= moduleRequest.MName,
                Credits= moduleRequest.Credits,
                ClassHoursPerWeek= moduleRequest.ClassHoursPerWeek,
                NumberOfWeeks= moduleRequest.NumberOfWeeks,
                StartDate= moduleRequest.StartDate,
                SelfStudyHours = CalculateSelfStudyHours(moduleRequest.Credits, moduleRequest.NumberOfWeeks, moduleRequest.NumberOfWeeks),
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)

            };

            await moduleAppDbContext.Modules.AddAsync(mod);
            await moduleAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        private int CalculateSelfStudyHours(int credits, int classHoursPerWeek, int numberOfWeeks)
        {
            // Perform the calculation
            return ((credits * 10) / numberOfWeeks) - classHoursPerWeek;
        }
    }
}
//The following method was taken from youtube
// Author: Sameer Saini
//Link: https://youtu.be/2Cp8Ti_f9Gk?si=ve2zdqpPaGJ2rfVO

// Troelsen,A and Japikse, P.2021. Foundational Priciples and Practices in Programming. 10th ed.New York: Welmoed Spahr