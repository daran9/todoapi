using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace TodoAPI.Test.Integration
{
    public static class Extensions
    {
        public static IServiceCollection OverrideService<T, TService>(this IServiceCollection services, ServiceLifetime lifetime)
            where TService : T
        {
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            services.Add(new ServiceDescriptor(typeof(T), typeof(TService), lifetime));
            return services;
        }
    }
}