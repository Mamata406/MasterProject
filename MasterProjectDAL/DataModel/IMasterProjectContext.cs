using Microsoft.EntityFrameworkCore;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectDAL.DataModel
{
    public interface IMasterProjectContext : IMasterProjectDbContext
    {
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Departments> Departments { get; set; }
       
    }
}
