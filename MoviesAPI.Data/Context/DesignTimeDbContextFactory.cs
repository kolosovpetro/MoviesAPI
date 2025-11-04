using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoviesAPI.Data.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MoviesContext>
{
    private readonly string _mangoDatabaseUrl;

    public DesignTimeDbContextFactory()
    {
        _mangoDatabaseUrl =
            "Data Source=DESKTOP-74EAN6J;Initial Catalog=MOVIES_DEV;Integrated Security=true;TrustServerCertificate=True;";
    }

    public MoviesContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<MoviesContext>();

        options.UseSqlServer(_mangoDatabaseUrl);

        return new MoviesContext(options.Options);
    }
}