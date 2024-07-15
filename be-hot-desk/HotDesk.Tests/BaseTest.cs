using HotDesk.Api.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace HotDesk.Tests
{
    public abstract class BaseTest
    {
        private readonly IServiceCollection services = DependencyInjection.GetServices();

        private IServiceProvider? serviceProvider;

        protected IServiceProvider ServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    serviceProvider = services.BuildServiceProvider();
                }
                return serviceProvider;
            }
        }

        protected IRepository repository => ServiceProvider.GetRequiredService<IRepository>();

        protected IMediator mediator => ServiceProvider.GetRequiredService<IMediator>();


        protected void Mock<T>(Mock<T> implementation) where T : class
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), implementation.Object);
            services.Replace(serviceDescriptor);
        }

        protected T GetService<T>() where T : class
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
