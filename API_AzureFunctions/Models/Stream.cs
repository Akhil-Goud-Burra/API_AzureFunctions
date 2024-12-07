using System;
using System.Collections.Generic;

namespace API_AzureFunctions.Models;

public partial class Stream
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
