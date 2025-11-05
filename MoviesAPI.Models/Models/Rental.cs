using System;

namespace MoviesAPI.Models.Models;

public class Rental
{
    public int CopyId { get; set; }
    public int ClientId { get; set; }
    public DateTime? DateOfRental { get; set; }
    public DateTime? DateOfReturn { get; set; }

    public virtual Client Client { get; set; }
    public virtual Copy Copy { get; set; }
}