using Core.Data;

namespace TaxCalculator.Module.Data.Entities;

public class TaxBand : BaseEntity
{
    public decimal LowerLimit { get; set; }
    public decimal? UpperLimit { get; set; }
    public decimal TaxRate { get; set; }
}