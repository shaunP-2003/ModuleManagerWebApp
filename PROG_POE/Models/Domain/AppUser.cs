using Microsoft.AspNetCore.Identity;
using System.Reflection;
using PROG_POE.Areas.Identity.Data;


namespace PROG_POE.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public ICollection<Modules> Modules { get; set; }
        public List<StudyHoursRecords> StudyHoursRecords { get; set; }
    }
}
