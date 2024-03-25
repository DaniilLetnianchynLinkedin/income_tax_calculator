namespace Core.Abstractions.Data.Entities;

public interface ISoftDeletable
{ 
    public bool IsSoftDeleted { get; set; }
}