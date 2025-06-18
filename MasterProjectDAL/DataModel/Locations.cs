using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class Locations
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public int? Zip { get; set; }

    public virtual ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();
}
