using System;
using CqrsApi.Auxiliaries.Auxiliaries;
using CqrsApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsApi.Data.Extensions
{
    public static class DataLayerWithPostgreSql
    {
        public static IServiceCollection AddDataLayerWithPostgreSql(this IServiceCollection services,
            IConfiguration configuration)
        {
            var environmentConnectionString = Environment.GetEnvironmentVariable("HEROKU_POSTGRE_CONNECTION_STRING");

            services.AddDbContext<PostgreContext>(options =>
                options.UseNpgsql(
                    StringParser.Convert(environmentConnectionString) ??
                    configuration.GetConnectionString("LOCAL_POSTGRES_CONNECTION_STRING")));

            services.AddTransient<DbContext, PostgreContext>();
            return services;
        }
    }
}