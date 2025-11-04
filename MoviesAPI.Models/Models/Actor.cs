using System;
using System.Collections.Generic;

namespace MoviesAPI.Models.Models
{
    public class Actor
    {
        public Actor()
        {
            Starrings = new HashSet<Starring>();
        }

        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Starring> Starrings { get; set; }
    }
}
