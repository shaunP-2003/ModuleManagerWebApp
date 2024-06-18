using System.ComponentModel.DataAnnotations;

namespace PROG_POE.Models.Domain
{
    public class StudyHoursVM
    {
        public Guid StudyRecordId { get; set; }


        public Guid ModuleId { get; set; }


        public string UserId { get; set; }

        public DateTime Date { get; set; }


        [Range(0, 24, ErrorMessage = "Please enter a valid number")]
        public int HoursSpent { get; set; }
    }
}
