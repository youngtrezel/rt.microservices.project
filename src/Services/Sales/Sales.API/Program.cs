using Sales.API.Data;
using Sales.API.Handlers;
using Sales.API.Interfaces;
using Sales.API.Setup;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Configurations;
using Sales.Repository.Interfaces;
using Sales.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
string name = typeof(Program).Assembly.GetName().Name;

//Serilog setup
Log.Information("Configuring Serilog");
var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .Build();

var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.WithProperty("Assembly", name)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationHelper.Initialize(builder.Configuration);
var connectionstring = ConfigurationHelper.config.GetSection("ConnectionString").Value;

Log.Information("Configuring database ({ApplicationContext})...", name);
builder.Services.AddTransient<DbContext, ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options
    => options.UseSqlServer(connectionstring, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(typeof(Program).Name);
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
    }));

builder.Services.AddScoped<IPlatesHandler, PlatesHandler>();
builder.Services.AddScoped<IPlateRepository, PlateRepository>();

var app = builder.Build();

try
{
    Log.Information("Updating database ({ApplicationContext})...", name);
    using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
    {
        if (context != null)
        {
            var contextLogger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContextSeed>>();
            var settings = scope.ServiceProvider.GetService<IOptions<AppSettings>>();
            context.Database.EnsureCreated();

            Log.Information("Seeding data ({ApplicationContext})...", name);
            new ApplicationDbContextSeed()
            .SeedAsync(context, builder.Environment, contextLogger, settings)
            .Wait();
        }
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", name);
}
finally
{
    Log.CloseAndFlush();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

