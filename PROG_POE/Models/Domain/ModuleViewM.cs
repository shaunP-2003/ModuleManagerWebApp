namespace PROG_POE.Models.Domain
{
    public class ModuleViewM
    {
        public Guid ModuleId { get; set; }
        public string MCode { get; set; }
        public string MName { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }
        public double SelfStudyHours { get; set; }

        public string UserId { get; set; }
    }
}
