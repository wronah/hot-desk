using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk;
using Microsoft.EntityFrameworkCore;

namespace HotDesk.Api
{
    public static class DependencyInjection
    {


        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HotDesk");
            services.AddDbContext<IRepository, HotDeskDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}
