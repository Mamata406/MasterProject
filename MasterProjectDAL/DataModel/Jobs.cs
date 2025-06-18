using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class Jobs
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? LocationId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? PostedDate { get; set; }

    public DateTime? ClosingDate { get; set; }

    public virtual Departments? Department { get; set; }

    public virtual Locations? Location { get; set; }
}
