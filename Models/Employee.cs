namespace EmployeesTableWeb.Models
{
    public class Employee
    {
        public required string Id { get; set; }
        public string? EmployeeName { get; set; }
        public int totalHours { get; set; }
        public string? TotalTimeWorked { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
    }
}
