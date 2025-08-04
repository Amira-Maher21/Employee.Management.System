using MediatR;

namespace Employee.Management.System.mediator
{
    

     
        public class LogEmployeeActionCommand : IRequest
        {
            public string Action { get; set; } // "Create", "Update", "Delete"
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; }

            public LogEmployeeActionCommand(string action, int employeeId, string employeeName)
            {
                Action = action;
                EmployeeId = employeeId;
                EmployeeName = employeeName;
            }
        }
}
