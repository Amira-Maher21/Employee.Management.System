namespace Employee.Management.System.DTOS
{
    public class LogHistoryDto
    {
        public string Action { get; set; } // "Create", "Update", "Delete"

        public int EmployeeId { get; set; }
        public int LogHistoryId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
