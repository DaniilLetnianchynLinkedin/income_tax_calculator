using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaxCalculatorApi.Contracts.Entities;

public class AppDbContext : DbContext
{
    public DbSet<TaxBand> TaxBands { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxBand>().HasData(
            new TaxBand { Id = 1, LowerLimit = 0, UpperLimit = 5000, TaxRate = 0 },
            new TaxBand { Id = 2, LowerLimit = 5000, UpperLimit = 20000, TaxRate = 20 },
            new TaxBand { Id = 3, LowerLimit = 20000, UpperLimit = null, TaxRate = 40 }
        );
    }
}
