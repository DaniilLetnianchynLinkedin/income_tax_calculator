using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Abstractions.Data.Entities;

namespace Core.Data;

public abstract class BaseDataContext : DbContext
{
    protected BaseDataContext(DbContextOptions options)
        : base(options)
    {
        EntitiesSoftDeletionEnabled = true;
    }
    
    protected abstract string DataSchema { get; }

    private bool EntitiesSoftDeletionEnabled { get; }
    
    public override int SaveChanges()
    {
        if (EntitiesSoftDeletionEnabled)
        {
            HandleSoftDelete();
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        if (EntitiesSoftDeletionEnabled)
        {
            HandleSoftDelete();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected void HandleSoftDelete()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entity in entities.Where(e => e.Metadata.ClrType.IsAssignableTo(typeof(ISoftDeletable))))
        {
            entity.State = EntityState.Modified;
            ((ISoftDeletable)entity.Entity).IsSoftDeleted = true;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set Data Schema
        modelBuilder.HasDefaultSchema(DataSchema);

        // For Each Entity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Soft Deletion Rule
            if (EntitiesSoftDeletionEnabled && entityType.ClrType.GetInterface(nameof(ISoftDeletable)) != null)
            {
                var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                var parameter = Expression.Parameter(entityType.ClrType);
                var methodInfo = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool))!;
                var efPropertyCall = Expression.Call(null, methodInfo, parameter, Expression.Constant(nameof(ISoftDeletable.IsSoftDeleted)));
                var body = Expression.MakeBinary(ExpressionType.Equal, efPropertyCall, Expression.Constant(false));
                var expression = Expression.Lambda(body, parameter);
                entityBuilder.HasQueryFilter(expression);
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}