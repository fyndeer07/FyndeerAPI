using FyndeerAPI.Api.HealthChecks;
using FyndeerAPI.Application.Common.Behaviours;
using FyndeerAPI.Infrastructure;
using FyndeerAPI.Infrastructure.Persistence;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting FyndeerAPI");

    var builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Host.UseSerilog((context, services, config) => config
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/fyndeerapi-.log", rollingInterval: RollingInterval.Day));

    // Controllers
    builder.Services.AddControllers();
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer((document, context, _) =>
        {
            document.Info.Title = "Fyndeer API";
            document.Info.Version = "v1";
            return Task.CompletedTask;
        });
    });

    // MediatR — scans Application assembly + registers pipeline behaviours
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(FyndeerAPI.Application.AssemblyMarker).Assembly);
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    });

    // EF Core + Infrastructure
    builder.Services.AddInfrastructure(builder.Configuration);

    // Health checks
    builder.Services.AddHealthChecks()
        .AddCheck<DatabaseHealthCheck>("database", tags: ["ready"]);

    var app = builder.Build();

    // Run migrations on startup
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    using (var scope = app.Services.CreateScope())
    {
        if (app.Environment.IsDevelopment())
            EnsureDatabaseExists(connectionString);

        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    // OpenAPI + Scalar — available in all environments
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Fyndeer API";
        options.Theme = ScalarTheme.Purple;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    // Liveness — always healthy if the process is running
    app.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = _ => false,
        ResponseWriter = WriteJsonResponse
    });

    // Readiness — healthy only when the database is reachable
    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = WriteJsonResponse
    });

    // Overall — all registered checks
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = WriteJsonResponse
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static Task WriteJsonResponse(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = report.Status == HealthStatus.Healthy ? 200 : 503;

    var result = new
    {
        status = report.Status.ToString(),
        checks = report.Entries.Select(e => new
        {
            name = e.Key,
            status = e.Value.Status.ToString(),
            description = e.Value.Description
        })
    };

    return context.Response.WriteAsync(JsonSerializer.Serialize(result));
}

static void EnsureDatabaseExists(string connectionString)
{
    var builder = new SqlConnectionStringBuilder(connectionString);
    var database = builder.InitialCatalog;
    builder.InitialCatalog = "master";

    using var connection = new SqlConnection(builder.ConnectionString);
    connection.Open();
    using var command = connection.CreateCommand();
    command.CommandText = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{database}') CREATE DATABASE [{database}]";
    command.ExecuteNonQuery();

    Log.Information("Database {Database} ensured", database);
}
