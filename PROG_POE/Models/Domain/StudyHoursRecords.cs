using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PROG_POE.Models.Domain
{
    public class StudyHoursRecords
    {
        [Key]
        public Guid StudyRecordId { get; set; }

        
        public Guid ModuleId { get; set; }

      
        public string UserId { get; set; }

        public DateTime Date { get; set; }

        
        [Range(0, 24, ErrorMessage = "Please enter a valid number")]
        public int HoursSpent { get; set; }

        // Navigation properties
        public Modules Modules { get; set; }
        public AppUser User { get; set; }
    }
}
