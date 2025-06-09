namespace Auto_Circuit.DTO
{
    public class WorkTimeGroupDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthStatus { get; set; }
        public List<WorkTimeDTo> Records { get; set; }
    }
}