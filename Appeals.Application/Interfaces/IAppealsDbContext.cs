using Appeals.Domain;
using Microsoft.EntityFrameworkCore;

namespace Appeals.Application.Interfaces
{
    public interface IAppealsDbContext
    {
        DbSet<Appeal> Appeals { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
