using Employee.Management.System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Management.System.Data.Configurations.FluentAPI
{
    public class LogHistoryConfig : IEntityTypeConfiguration<LogHistory>
    {
        public void Configure(EntityTypeBuilder<LogHistory> builder)
        {
            builder.ToTable("LogHistories");

            builder.HasKey(l => l.ID);

            builder.Property(l => l.Action)
                   .IsRequired();

            builder.Property(l => l.EmployeeName)
                   .IsRequired();

            builder.Property(l => l.Timestamp)
                   .IsRequired();

            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.LogHistories)
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
