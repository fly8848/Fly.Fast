namespace Fly.Fast.Domain;

public interface IEntity<T> : IEntity
{
    T Id { get; init; }
}

public interface IEntity
{
    
}