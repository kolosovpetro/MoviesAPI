using System.Reflection;
using AutoMapper;
using CqrsApi.Core.Extensions;
using CqrsApi.Requests.Command;
using CqrsApi.Requests.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CqrsApi.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDataLayerWithPostgreSql(Configuration);
            services.AddMediatR(typeof(GetAllMoviesHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(PostMovieCommand).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CQRS Movies API", Version = "v1"});
            });

            // allows to connect from front end to api
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // allows to connect from front end to api
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
}