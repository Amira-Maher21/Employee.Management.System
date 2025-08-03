using Employee.Management.System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Management.System.Data.Configurations.FluentAPI
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employe>
    {
        public void Configure(EntityTypeBuilder<Employe> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.ID);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Email)
                   .IsRequired();

            builder.Property(e => e.HireDate)
                   .IsRequired();

            builder.Property(e => e.Status)
                   .IsRequired()
                   .HasConversion<string>(); // يخزن enum كـ string

            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .HasPrincipalKey(d => d.ID)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
 