using System.Collections.Generic;

namespace CqrsApi.Models.Models
{
    public class Movie
    {
        public Movie()
        {
            Copies = new HashSet<Copy>();
            Starrings = new HashSet<Starring>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public float Price { get; set; }

        public virtual ICollection<Copy> Copies { get; set; }
        public virtual ICollection<Starring> Starrings { get; set; }
    }
}
