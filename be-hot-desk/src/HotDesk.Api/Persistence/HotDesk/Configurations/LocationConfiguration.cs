using HotDesk.Api.Persistence.HotDesk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotDesk.Api.Persistence.HotDesk.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(256);

            builder
                .HasMany(x => x.Desks)
                .WithOne()
                .HasForeignKey(x => x.LocationsId);
        }
    }
}
