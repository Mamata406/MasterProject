using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel
{
    public interface IMasterProjectDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellation = default(CancellationToken));
        DatabaseFacade Database { get; }
    }
}
