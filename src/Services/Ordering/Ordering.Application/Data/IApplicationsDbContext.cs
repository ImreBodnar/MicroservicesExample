using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data
{
    public interface IApplicationsDbContext
    {
        DbSet<Customer> Customers { get; }

        DbSet<Product> Products { get; }

        DbSet<Order> Orders { get; }

        DbSet<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}