namespace Fly.Fast.Domain;

public interface IHasCreated
{
    DateTime CreatedTime { get; set; }
    string? CreatedBy { get; set; }
}