﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using TodoApi.Application.Extensions;
using TodoApi.Web.HealthCheck;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .AddJsonFile("appsettings.json", true, true)
        .AddEnvironmentVariables()
        .AddUserSecrets<TodoApi.Program>()
        .AddCommandLine(args)
        .Build();

    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHealthChecks()
        .AddCheck<TodoHealthCheck>("todo_health_check");

    builder.Services.AddDependencies(builder.Configuration, builder.Environment);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health");

    app.UseSerilogRequestLogging();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

namespace TodoApi
{
    public class Program
    {
    }
}