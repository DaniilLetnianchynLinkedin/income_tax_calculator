using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions.Data.Entities;

namespace Core.Data;

public class BaseEntity : IBaseEntity, ISoftDeletable
{
     protected BaseEntity()
     {
     }

     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public virtual int Id { get; set; }

     public bool IsSoftDeleted { get; set; } = false;

     public override int GetHashCode() => Id.GetHashCode();
}
