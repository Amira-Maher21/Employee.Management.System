using Employee.Management.System.Data.Configurations.FluentAPI;
using Employee.Management.System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Employee.Management.System.Data
{
    public class StoreContext: DbContext
    {
 

 
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

            // DbSets
            public DbSet<Employe> Employees { get; set; }
            public DbSet<Department> Departments { get; set; }
            public DbSet<LogHistory> LogHistories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Apply Fluent API Configurations
                modelBuilder.ApplyConfiguration(new DepartmentConfig());
                modelBuilder.ApplyConfiguration(new EmployeeConfig());
                modelBuilder.ApplyConfiguration(new LogHistoryConfig());

                base.OnModelCreating(modelBuilder);
            }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-2I0OH7I\\SQLEXPRESS;Initial Catalog=Employee.Management.System; User Id=sa;Password=123;TrustServerCertificate=true")
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

    }



}
