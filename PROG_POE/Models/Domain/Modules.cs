using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG_POE.Models.Domain
{
    public class Modules
    {
        [Key]
        public Guid ModuleId { get; set; }
        public string MCode { get; set; }
        public string MName { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }

        public double SelfStudyHours { get; set; }

        // Foreign key to link the module to a specific user
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public List<StudyHoursRecords> StudyHoursRecords { get; set; }

    }
}
