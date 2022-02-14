using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Serilog;
using MediatR;
using TodoApi.Domain.Repository;
using TodoApi.Infrastructure.Repository;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }


        private const string HttpHostDockerInternal = "http://localstack:4566";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
        {
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();

            if(environment.IsDevelopment())
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {   
                    var clientConfig = new AmazonDynamoDBConfig{ ServiceURL = HttpHostDockerInternal };
                    return new AmazonDynamoDBClient(clientConfig);
                });

            }else
            {
                services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();
            }

            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddTransient<ITodoRepository, DynamoDBTodoRepository>();

            services.AddHealthChecks()
                .AddCheck<TodoHealthCheck>("todo_health_check");

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSerilogRequestLogging();
        }
    }
}
