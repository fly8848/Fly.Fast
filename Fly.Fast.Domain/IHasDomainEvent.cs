namespace Fly.Fast.Domain;

public interface IHasDomainEvent
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}