using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class Departments
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();
}
