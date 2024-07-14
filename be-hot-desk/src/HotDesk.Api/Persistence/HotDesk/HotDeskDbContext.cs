using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotDesk.Api.Persistence.HotDesk
{
    public class HotDeskDbContext : DbContext, IRepository
    {
        public HotDeskDbContext(DbContextOptions<HotDeskDbContext> options) : base(options)
        {
        }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
