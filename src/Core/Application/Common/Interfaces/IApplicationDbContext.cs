using DAT154Oblig4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAT154Oblig4.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Customer> Customers { get; set; }

    DbSet<Room> Rooms { get; set; }

    DbSet<ServiceTask> ServiceTasks { get; set; }

    DbSet<Booking> Bookings { get; set; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
