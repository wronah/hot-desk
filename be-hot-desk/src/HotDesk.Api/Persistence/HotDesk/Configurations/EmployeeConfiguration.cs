using HotDesk.Api.Persistence.HotDesk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotDesk.Api.Persistence.HotDesk.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).HasMaxLength(128);
            builder.Property(x => x.LastName).HasMaxLength(128);

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Employees)
                .UsingEntity(x => x.ToTable("employee_roles"));
        }
    }
}
