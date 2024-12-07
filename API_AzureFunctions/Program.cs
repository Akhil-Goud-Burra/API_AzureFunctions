using API_AzureFunctions.Models;
using API_AzureFunctions.RepositoryPattern.IRepository;
using API_AzureFunctions.RepositoryPattern.IRepositoryImplementation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add DbContext with SQL Server configuration
        services.AddDbContext<MyDbContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            options.UseSqlServer(connectionString);
        });

        // Add Scoped service for repository
        services.AddScoped<IRepositoryStream, IRepositoryStreamImplementation>();

    })
    .Build();

host.Run();
