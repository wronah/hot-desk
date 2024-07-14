using HotDesk.Api.Persistence.HotDesk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotDesk.Api.Persistence.HotDesk.Configurations
{
    public class DeskConfiguration : IEntityTypeConfiguration<Desk>
    {
        public void Configure(EntityTypeBuilder<Desk> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.AddDate).HasColumnType("timestamp without time zone");
            builder.Property(x => x.RemoveDate).HasColumnType("timestamp without time zone");
            builder.Property(x => x.StartReservationDate).HasColumnType("timestamp without time zone");
            builder.Property(x => x.EndReservationDate).HasColumnType("timestamp without time zone");

            builder
                .HasOne(x => x.Employee)
                .WithOne(x => x.Desk)
                .HasForeignKey<Employee>(x => x.DesksId);
        }
    }
}
