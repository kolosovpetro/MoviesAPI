using CqrsApi.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CqrsApi.Data.Context
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        
        public virtual DbSet<Client> Clients { get; set; }
        
        public virtual DbSet<Copy> Copies { get; set; }
        
        public virtual DbSet<Employee> Employees { get; set; }
        
        public virtual DbSet<Movie> Movies { get; set; }
        
        public virtual DbSet<Rental> Rentals { get; set; }
        
        public virtual DbSet<Starring> Starrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Copy>(entity =>
            {
                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Copies)
                    .HasForeignKey(d => d.MovieId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId);

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Salary);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasData(
                    new Movie(1, "Star Wars Episode IV: A New Hope", 1979, 12, 10f),
                    new Movie(2, "Ghostbusters", 1984, 12, 5.5f),
                    new Movie(3, "Terminator", 1984, 15, 8.5f),
                    new Movie(4, "Taxi Driver", 1976, 17, 5f),
                    new Movie(5, "Platoon", 1986, 18, 5f),
                    new Movie(6, "Frantic", 1988, 15, 8.5f),
                    new Movie(7, "Ronin", 1998, 13, 9.5f),
                    new Movie(8, "Analyze This", 1999, 16, 10.5f),
                    new Movie(9, "Leon: the Professional", 1994, 16, 8.5f),
                    new Movie(10, "Mission Impossible", 1996, 13, 8.5f));
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.CopyId });

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.ClientId);

                entity.HasOne(d => d.Copy)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CopyId);
            });

            modelBuilder.Entity<Starring>(entity =>
            {
                entity.HasKey(e => new {e.ActorId, e.MovieId});

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Starrings)
                    .HasForeignKey(d => d.ActorId);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Starrings)
                    .HasForeignKey(d => d.MovieId);
            });
        }
    }
}