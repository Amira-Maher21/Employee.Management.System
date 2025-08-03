using System.ComponentModel.DataAnnotations;

namespace Employee.Management.System.Models
{
    public class LogHistory : BaseModel
    {
 
         public string Action { get; set; } // "Create", "Update", "Delete"

        public int EmployeeId { get; set; }

         public string EmployeeName { get; set; }

        public DateTime Timestamp { get; set; }
         public Employe Employee { get; set; }
    }

}
