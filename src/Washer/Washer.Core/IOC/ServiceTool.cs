using Microsoft.Extensions.DependencyInjection;
using System;

namespace Washer.Core.IOC
{
    public static class ServiceTool
    {
        private static IServiceProvider ServiceProvider { get; set; }
        public static IServiceProvider Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return ServiceProvider;
        }
        public static T resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }

}
