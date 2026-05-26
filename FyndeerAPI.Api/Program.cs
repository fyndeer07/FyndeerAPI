using FyndeerAPI.Application.Common.Behaviours;
using FyndeerAPI.Infrastructure;
using FyndeerAPI.Infrastructure.Persistence;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;
using Serilog;

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
