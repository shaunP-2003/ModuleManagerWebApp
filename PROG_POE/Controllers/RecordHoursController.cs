using Microsoft.AspNetCore.Mvc;
using PROG_POE.Data;
using PROG_POE.Models.Domain;
using System.Security.Claims;

namespace PROG_POE.Controllers
{
    public class RecordHoursController : Controller
    {
        private readonly ModuleAppDbContext moduleAppDbContext;
        private readonly ILogger<HomeController> _logger;

        public RecordHoursController(ModuleAppDbContext moduleAppDbContext, ILogger<HomeController> logger)
        {
            this.moduleAppDbContext = moduleAppDbContext;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult RecordHours()
        {
            //The following method was taken from stackoverflow
            // Author: Matěj Štágl
            //Link: https://stackoverflow.com/questions/57727635/how-to-pass-selected-query-list-using-viewbag
            // Displaying list through to set viewbag
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve modules for the current user
            var modules = moduleAppDbContext.Modules
                .Where(m => m.UserId == currentUserId)
                .ToList();


            // Create a dictionary to store total hours and remaining self-study hours for each module
            var hoursDictionary = new Dictionary<Guid, Tuple<int, double>>();

            foreach (var module in modules)
            {
                // Calculate total hours for each module
                var totalHoursForModule = moduleAppDbContext.StudyHoursRecords
                    .Where(record => record.UserId == currentUserId && record.ModuleId == module.ModuleId)
                    .Sum(record => record.HoursSpent);

                // Calculate remaining self-study hours for each module
                var remainingSelfStudyHours = module.SelfStudyHours - totalHoursForModule;

                // Store total hours and remaining self-study hours in the dictionary
                hoursDictionary[module.ModuleId] = new Tuple<int, double>(totalHoursForModule, remainingSelfStudyHours);
            }

            // Pass modules and hours dictionary to the view
            ViewBag.Modules = modules;
            ViewBag.HoursDictionary = hoursDictionary;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecordHours(StudyHoursVM viewModel)
        {
            //The following method was taken from stackoverflow
            // Author: Matěj Štágl
            //Link: https://stackoverflow.com/questions/57727635/how-to-pass-selected-query-list-using-viewbag
            // Displaying list through to set viewbag

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                
               

                // Retrieve modules for the current user
                var modules = moduleAppDbContext.Modules
                    .Where(m => m.UserId == currentUserId)
                    .ToList();

                // Set modules in ViewBag then the viewbag will be used to iterate through the viewbag and then display it on the option list 
                ViewBag.Modules = modules;

                
            }

            // Retrieve the module
            var module = await moduleAppDbContext.Modules.FindAsync(viewModel.ModuleId);
          

            // Create a new StudyRecord
            var studyRecord = new StudyHoursRecords
            {
                StudyRecordId = Guid.NewGuid(),
                ModuleId = viewModel.ModuleId,
                UserId = currentUserId,
                Date = viewModel.Date,
                HoursSpent = viewModel.HoursSpent,
                // Add other properties as needed
            };

            // Save changes to the database
            await moduleAppDbContext.StudyHoursRecords.AddAsync(studyRecord);
            await moduleAppDbContext.SaveChangesAsync();

            return RedirectToAction("RecordHours", "RecordHours");
        }

    }
}
