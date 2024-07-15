using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Common.Options;
using HotDesk.Api.Persistence.HotDesk;
using HotDesk.Api.Services.UserResolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace HotDesk.Api
{
    public static class DependencyInjection
    {
        public static void ConfigureOptions(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.ConfigureOption<JwtSettings>();
        }
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserResolver, UserResolver>();

            return services;
        }

        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            return services;
        }


        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HotDesk");
            services.AddDbContext<IRepository, HotDeskDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }
        private static WebApplicationBuilder ConfigureOption<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            webApplicationBuilder.Services.Configure<T>(webApplicationBuilder.Configuration.GetSection(typeof(T).Name));
            return webApplicationBuilder;
        }

    }
}
