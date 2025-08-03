using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;

namespace Employee.Management.System.Data.Configurations.FluentAPI
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-2I0OH7I\\SQLEXPRESS;Initial Catalog=Employee.Management.System; User Id=sa;Password=123;TrustServerCertificate=true");

            return new StoreContext(optionsBuilder.Options);
        }
    }

}
