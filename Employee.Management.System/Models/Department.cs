using System.ComponentModel.DataAnnotations;

namespace Employee.Management.System.Models
{
    public class Department: BaseModel
    {
 
         
        public string Name { get; set; }

         public ICollection<Employe> Employees { get; set; }
    }

}
