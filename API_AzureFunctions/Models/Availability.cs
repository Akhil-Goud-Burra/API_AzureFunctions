using System;
using System.Collections.Generic;

namespace API_AzureFunctions.Models;

public partial class Availability
{
    public int Id { get; set; }

    public string Quantity { get; set; } = null!;

    public bool Status { get; set; }

    public int BookId { get; set; }

    public virtual Book Book { get; set; } = null!;
}
