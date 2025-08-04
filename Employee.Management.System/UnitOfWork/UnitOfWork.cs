using Employee.Management.System.Data;
using Employee.Management.System.Models;
using Employee.Management.System.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Employee.Management.System.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        public IRepository<Employe> Employees { get; private set; }

        public IRepository<Department> Departments { get; private set; }

        public IRepository<LogHistory> LogHistories { get; private set; }

        public UnitOfWork(StoreContext context,
                          IRepository<Employe> employeeRepo,
                          IRepository<Department> departmentRepo,
                          IRepository<LogHistory> logRepo)
        {
            _context = context;
            Employees = employeeRepo;
            Departments = departmentRepo;
            LogHistories = logRepo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
