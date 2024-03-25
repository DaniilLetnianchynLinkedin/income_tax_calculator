using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaxCalculator.Module.Data.Entities.Configuration;

public class TaxBandConfiguration : IEntityTypeConfiguration<TaxBand>
{
    public void Configure(EntityTypeBuilder<TaxBand> builder)
    {
        builder.HasKey(x => x.Id);
    }
}