
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                s.AddOcelot();

                s.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder.WithOrigins(["http://localhost:4200"])
                                            .AllowAnyMethod()
                                            .AllowAnyHeader());
                });

            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseCors("AllowSpecificOrigin");
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();