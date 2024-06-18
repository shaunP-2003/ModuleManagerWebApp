using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROG_POE.Data;
using PROG_POE.Models;
using PROG_POE.Models.Domain;
using System.Diagnostics;
using System.Security.Claims;

namespace PROG_POE.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ModuleAppDbContext moduleAppDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ModuleAppDbContext moduleAppDbContext, ILogger<HomeController> logger)
        {
            this.moduleAppDbContext = moduleAppDbContext;
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> ModulesView()
        {//The following method was taken from stackoverflow
            // Author: Matěj Štágl
            //Link: https://stackoverflow.com/questions/57727635/how-to-pass-selected-query-list-using-viewbag
            // Displaying list through to set viewbag

            //fetching the userID
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //The following method was taken from stackoverflow
            // Author: TanvirArjel
            //Link:https://stackoverflow.com/questions/30701006/how-to-get-the-current-logged-in-user-id-in-asp-net-core
            // Getting the userId

            // fetching the users modules 
            var modules = moduleAppDbContext.Modules
               .Where(m => m.UserId == currentUserId)
               .ToList();



            //The following method was taken from Website
            // Author: Marcus Rath
            //Link: https://blog.matrixpost.net/using-list-tuples-in-c/
            // Combine the two lists into a tuple or a custom ViewModel
            var hoursDictionary = new Dictionary<Guid, double>();

            foreach (var module in modules)
            {
                // Calculate total hours for each module
                var totalHoursForModule = moduleAppDbContext.StudyHoursRecords
                    .Where(record => record.UserId == currentUserId && record.ModuleId == module.ModuleId)
                    .Sum(record => record.HoursSpent);

                //The following SUM method was taken from Stackoverflow
                // Author: hemantsharma
                //Link: https://stackoverflow.com/questions/38931374/how-to-perform-sum-operation-in-entity-framework

                // Calculate remaining self-study hours for each module
                var remainingSelfStudyHours = module.SelfStudyHours - totalHoursForModule;

                // Store total hours and remaining self-study hours in the according to moduleId
                hoursDictionary[module.ModuleId] = remainingSelfStudyHours;
            }

            
            ViewBag.Modules = modules;
            ViewBag.HoursDictionary = hoursDictionary;

            //The following method was taken from youtube
            // Author: Nick Proud
            //Link: https://youtu.be/HVuylC-XMDM?si=d6-VwztXwpvLxgjz

            //Display the Tabel Modules 
            var sqlQuery = $@"
                SELECT 
                    m.ModuleId,
                    m.MCode,
                    m.MName,
                    m.Credits,
                    m.ClassHoursPerWeek,
                    m.NumberOfWeeks,
                    m.StartDate,
                    m.SelfStudyHours,
                    m.UserId
                FROM 
                    Modules m
                JOIN 
                    AspNetUsers u ON u.Id = m.UserId
                WHERE
                    m.UserId = '{currentUserId}'";

            // Retrieve modules for the current user
            var moduleList = await moduleAppDbContext.Modules
            .FromSqlRaw(sqlQuery)
            .Select(result => new ModuleViewM
            {
                    ModuleId = result.ModuleId,
                    MCode = result.MCode,
                    MName = result.MName,
                    Credits = result.Credits,
                    ClassHoursPerWeek = result.ClassHoursPerWeek,
                    NumberOfWeeks = result.NumberOfWeeks,
                    StartDate = result.StartDate,
                    SelfStudyHours = result.SelfStudyHours,
                    UserId = result.UserId
                })
                .ToListAsync();

            return View(moduleList);
        }
        
        [HttpGet]
        public async Task<IActionResult> StudyHoursChart()
        {
            //The following method was taken from stackoverflow
            // Author: Matěj Štágl
            //Link: https://stackoverflow.com/questions/57727635/how-to-pass-selected-query-list-using-viewbag
            // Displaying list through to set viewbag

            // Get data from the database or any other source
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var modules = moduleAppDbContext.Modules
                .Where(m => m.UserId == currentUserId)
                .ToList();

            var totalHoursDictionary = new Dictionary<Guid, int>();

            // Loop through each module and calculate total hours
            foreach (var module in modules)
            {
                // Calculate total hours for each module asynchronously
                var totalHoursForModule = await moduleAppDbContext.StudyHoursRecords
                    .Where(record => record.UserId == currentUserId && record.ModuleId == module.ModuleId)
                    .SumAsync(record => record.HoursSpent);

                // Store total hours in the dictionary
                totalHoursDictionary[module.ModuleId] = totalHoursForModule;
            }

            // Pass modules and total hours dictionary to the view
            ViewBag.Modules = modules;
            ViewBag.TotalHoursDictionary = totalHoursDictionary;

            return View();
        }

    }
}
// Troelsen,A and Japikse, P.2021. Foundational Priciples and Practices in Programming. 10th ed.New York: Welmoed Spahr