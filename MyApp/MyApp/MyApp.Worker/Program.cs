using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyApp.Training;
using MyApp.Worker;
using Serilog;
using Serilog.Events;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var migrationAssemblyName = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        //This will help us to use worker service as windows service
        .UseWindowsService()
        //Auto Configuration
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //Serilog Configuration
        .UseSerilog()
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new WorkerModule(configuration));
            builder.RegisterModule(new TrainingModule(connectionString, migrationAssemblyName));
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .Build();

    Log.Information("Application starting up!");

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Error(ex.Message);
}
finally
{
    Log.CloseAndFlush();
}
