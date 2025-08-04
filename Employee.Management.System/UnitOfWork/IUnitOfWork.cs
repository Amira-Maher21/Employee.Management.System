using Employee.Management.System.Models;
using Employee.Management.System.Repositories;

namespace Employee.Management.System.UnitOfWork
{
    
        public interface IUnitOfWork : IDisposable
        {
            IRepository<Employe> Employees { get; }
            IRepository<Department> Departments { get; }
            IRepository<LogHistory> LogHistories { get; }

            void SaveChanges();
            Task<int> CompleteAsync();
        }
    }
