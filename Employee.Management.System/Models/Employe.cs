using System.ComponentModel.DataAnnotations;

namespace Employee.Management.System.Models
{
    public class Employe: BaseModel
    {
 
         
        public string Name { get; set; }

         
        [EmailAddress]
        public string Email { get; set; }

         public DateTime HireDate { get; set; }

         public EmployeeStatus Status { get; set; }

         public int DepartmentId { get; set; }

         public Department Department { get; set; }

         public ICollection<LogHistory> LogHistories { get; set; }
    }

}
