using System.Collections.Generic;

namespace MoviesAPI.Models.Models;

public class Movie
{
    public Movie()
    {
        Copies = new HashSet<Copy>();
        Starrings = new HashSet<Starring>();
    }
        
    // new Movie(1, "Star Wars Episode IV: A New Hope", 1979, 12, 10f),

    public Movie(int movieId, string title, int year, int ageRestriction, float price)
    {
        MovieId = movieId;
        Title = title;
        Year = year;
        AgeRestriction = ageRestriction;
        Price = price;
    }

    public int MovieId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int AgeRestriction { get; set; }
    public float Price { get; set; }

    public virtual ICollection<Copy> Copies { get; set; }
    public virtual ICollection<Starring> Starrings { get; set; }
}