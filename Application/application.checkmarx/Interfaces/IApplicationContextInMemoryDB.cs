using domain.entities.checkmarx;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace application.checkmarx.Interfaces
{
    public interface IApplicationContextInMemoryDB
    {
        DbSet<Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
