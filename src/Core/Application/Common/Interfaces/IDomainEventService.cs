using DAT154Oblig4.Domain.Common;

namespace DAT154Oblig4.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
