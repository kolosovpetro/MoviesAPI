using System;
using System.Collections.Generic;

namespace MoviesAPI.Models.Models
{
    public class Client
    {
        public Client()
        {
            Rentals = new HashSet<Rental>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
