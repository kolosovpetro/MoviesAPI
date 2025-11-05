using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Data.Context;
using MoviesAPI.Models.Models;

namespace MoviesAPI.Core;

public static class DatabaseMigrator
{
    public static void MigrateDatabase(this IApplicationBuilder app, bool useSqlServerS)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<MoviesContext>();

        if (context == null)
        {
            throw new InvalidOperationException("Database context is NULL at Migrator service.");
        }

        try
        {
            if (useSqlServerS)
            {
                context.Database.Migrate();
            }
            else
            {
                var movies = new List<Movie>
                {
                    new Movie(1, "Star Wars Episode IV: A New Hope", 1979, 12, 10f),
                    new Movie(2, "Ghostbusters", 1984, 12, 5.5f),
                    new Movie(3, "Terminator", 1984, 15, 8.5f),
                    new Movie(4, "Taxi Driver", 1976, 17, 5f),
                    new Movie(5, "Platoon", 1986, 18, 5f),
                    new Movie(6, "Frantic", 1988, 15, 8.5f),
                    new Movie(7, "Ronin", 1998, 13, 9.5f),
                    new Movie(8, "Analyze This", 1999, 16, 10.5f),
                    new Movie(9, "Leon: the Professional", 1994, 16, 8.5f),
                    new Movie(10, "Mission Impossible", 1996, 13, 8.5f)
                };

                context.Movies.AddRange(movies);

                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}