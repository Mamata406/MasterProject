using System;
using System.Collections.Generic;

namespace MasterProjectDAL.DataModel;

public partial class Login
{
    public int Username { get; set; }

    public string Password { get; set; } = null!;
}
