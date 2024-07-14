using HotDesk.Api.Persistence.HotDesk.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotDesk.Api.Common.Interfaces
{
    public interface IRepository
    {
        public DbSet<Desk> Desks { get; }
        public DbSet<Location> Locations { get; }
        public DbSet<Employee> Employees { get; }   
        public DbSet<Role> Roles { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
