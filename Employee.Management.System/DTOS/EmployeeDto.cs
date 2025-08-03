using Employee.Management.System.Models;
using System.ComponentModel.DataAnnotations;

namespace Employee.Management.System.DTOS
{
     
        public class EmployeeDto
        {
            public int EmployeeId { get; set; } 
            public string Name { get; set; }
            public string Email { get; set; }
            public int DepartmentId { get; set; }
            public DateTime HireDate { get; set; }
            public EmployeeStatus Status { get; set; }  


 

       
 
 
 
     }
    

}
