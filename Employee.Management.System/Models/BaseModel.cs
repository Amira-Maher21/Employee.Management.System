using System.ComponentModel.DataAnnotations;

namespace Employee.Management.System.Models
{
    public class BaseModel
    {
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

         public bool IsDeleted { get; set; } = false;

    }

    

}
