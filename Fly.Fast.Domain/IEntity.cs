namespace Fly.Fast.Domain;

public interface IEntity<T> : IEntity
{
    T Id { get; init; }
}

public interface IEntity
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}