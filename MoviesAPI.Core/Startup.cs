using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MoviesAPI.Data.Context;
using MoviesAPI.Requests.QueryHandlers;

namespace MoviesAPI.Core;

public class Startup
{
    private const string DefaultVersion = "1.0.0.0";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetAllMoviesHandler).GetTypeInfo().Assembly));

        var useSqlServer = GetConfigurationValue<bool>(Configuration, "UseSqlServerDatabase");

        if (useSqlServer)
        {
            ConfigureSqlServer(services);
        }
        else
        {
            ConfigureInMemoryDatabase(services);
        }

        services.AddAutoMapper(typeof(Startup));

        services.AddSwaggerGen(c =>
        {
            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? DefaultVersion;

            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"CQRS Movies API {version}", Version = $"v{version}" });
        });

        services.AddCors();
    }

    private static void ConfigureInMemoryDatabase(IServiceCollection services)
    {
        services.AddDbContext<MoviesContext>(o => o.UseInMemoryDatabase("MoviesDatabase"));
    }

    private void ConfigureSqlServer(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("SqlServerConnectionString")
                               ?? Configuration.GetConnectionString("SqlServerConnectionString");

        services.AddDbContext<MoviesContext>(opt => opt.UseSqlServer(connectionString));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        var useSqlServer = GetConfigurationValue<bool>(Configuration, "UseSqlServerDatabase");

        app.MigrateDatabase(useSqlServer);

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });


        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static T GetConfigurationValue<T>(IConfiguration configuration, string key)
    {
        var fromEnvironment = Environment.GetEnvironmentVariable(key);

        if (fromEnvironment != null && !string.IsNullOrEmpty(fromEnvironment))
        {
            var parseEnvironment = (T)Convert.ChangeType(fromEnvironment, typeof(T));
            return parseEnvironment;
        }

        var fromConfiguration = configuration.GetValue<T>(key);

        return fromConfiguration;
    }
}