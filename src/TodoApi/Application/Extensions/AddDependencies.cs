using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Domain.Repository;
using TodoApi.Infrastructure.Repository;

namespace TodoApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
    private const string HttpHostDockerInternal = "http://localstack:4566";

    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
        if (environment.IsDevelopment())
        {
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var clientConfig = new AmazonDynamoDBConfig { ServiceURL = HttpHostDockerInternal };
                return new AmazonDynamoDBClient(clientConfig);
            });
        }
        else
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
        }

        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        services.AddTransient<ITodoRepository, DynamoDBTodoRepository>();

        services.AddMediator();

        return services;
    }
}