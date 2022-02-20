using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Domain.Repository;
using TodoAPI.Test.Integration.Infrastructure.Repository;

namespace TodoAPI.Test.Integration
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {

        public static CustomWebApplicationFactory<TStartup> Create()
            => new CustomWebApplicationFactory<TStartup>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => services         
                .OverrideService<ITodoRepository, InMemoryTodoRepository>(ServiceLifetime.Scoped)
            );
        }
    }
}