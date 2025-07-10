using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DocumentProcessingApp.Application.Interfaces;
using DocumentProcessingApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();

        services.AddScoped<IDocKeyService, DocKeyApiService>();
        services.AddScoped<ILogProcessService, LogProcessApiService>();
        services.AddScoped<DocumentProcessingService>();

        services.AddHostedService<DocumentProcessingApp.Worker.Worker>();
    })
    .Build()
    .Run();
