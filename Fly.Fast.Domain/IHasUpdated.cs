namespace Fly.Fast.Domain;

public interface IHasUpdated
{
    DateTime? UpdatedTime { get; set; }
    string? UpdatedBy { get; set; }
}