using MediatR;

namespace Fly.Fast.Domain;

public abstract record DomainEvent : INotification
{
}