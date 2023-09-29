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

        var connectionString = Environment.GetEnvironmentVariable("SqlServerConnectionString")
                               ?? Configuration.GetConnectionString("SqlServerConnectionString");

        services.AddDbContext<MoviesContext>(opt => opt.UseSqlServer(connectionString));

        services.AddAutoMapper(typeof(Startup));

        services.AddSwaggerGen(c =>
        {
            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? DefaultVersion;

            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"CQRS Movies API {version}", Version = $"v{version}" });
        });

        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        var envVar = Environment.GetEnvironmentVariable("ShouldMigrate");

        var shouldMigrate = envVar == null
            ? Configuration.GetValue<bool>("ShouldMigrate")
            : bool.Parse(envVar);

        app.MigrateDatabase(shouldMigrate);

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
}