using HotDesk.Api;
using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotDesk.Tests
{
    internal static class DependencyInjection
    {
        public static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            services.AddDependencyInjection();
            services.RegisterMemoryDbContext();
            services.AddLogging();

            return services;
        }

        private static IServiceCollection RegisterMemoryDbContext(this IServiceCollection services)
        {
            var dbContextOptions = new DbContextOptionsBuilder<HotDeskDbContext>();
            dbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new HotDeskDbContext(dbContextOptions.Options);
            dbContext.Database.EnsureDeleted();

            services.AddSingleton<IRepository>(dbContext);
            return services;
        }
    }

}
