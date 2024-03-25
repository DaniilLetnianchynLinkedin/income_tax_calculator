using Microsoft.EntityFrameworkCore;
using Core.Data;
using TaxCalculator.Module.Constants;
using TaxCalculator.Module.Data.Entities;
using TaxCalculator.Module.Data.Entities.Configuration;

namespace TaxCalculator.Module.Data;

public class TaxCalculatorDataContext : BaseDataContext
{
    public TaxCalculatorDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TaxBand> TaxBands { get; set; }

    protected override string DataSchema => DataSchemas.TaxCalculator;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaxBandConfiguration());
        modelBuilder.Entity<TaxBand>().HasData(
            new TaxBand { Id = 1, LowerLimit = 0, UpperLimit = 5000, TaxRate = 0 },
            new TaxBand { Id = 2, LowerLimit = 5000, UpperLimit = 20000, TaxRate = 20 },
            new TaxBand { Id = 3, LowerLimit = 20000, UpperLimit = null, TaxRate = 40 }
        );
        base.OnModelCreating(modelBuilder);
    }
}