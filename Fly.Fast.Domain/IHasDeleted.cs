namespace Fly.Fast.Domain;

public interface IHasDeleted
{
    bool IsDeleted { get; set; }
    DateTime? DeletedTime { get; set; }
    string? DeletedBy { get; set; }
}