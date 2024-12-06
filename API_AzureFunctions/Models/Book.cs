using System;
using System.Collections.Generic;

namespace API_AzureFunctions.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int StreamId { get; set; }

    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    public virtual Stream Stream { get; set; } = null!;
}
