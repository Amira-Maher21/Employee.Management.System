using Employee.Management.System.Models;
using Employee.Management.System.Repositories;
using MediatR;

namespace Employee.Management.System.mediator
{
    
        public class LogEmployeeActionHandler : IRequestHandler<LogEmployeeActionCommand>
        {
            private readonly IRepository<LogHistory> _logHistoryRepository;

            public LogEmployeeActionHandler(IRepository<LogHistory> logHistoryRepository)
            {
                _logHistoryRepository = logHistoryRepository;
            }

            public async Task<Unit> Handle(LogEmployeeActionCommand request, CancellationToken cancellationToken)
            {
                var log = new LogHistory
                {
                    Action = request.Action,
                    EmployeeId = request.EmployeeId,
                    EmployeeName = request.EmployeeName,
                    Timestamp = DateTime.UtcNow
                };

                _logHistoryRepository.Add(log);
                _logHistoryRepository.SaveChanges();

                return Unit.Value;
            }
        }
    }
