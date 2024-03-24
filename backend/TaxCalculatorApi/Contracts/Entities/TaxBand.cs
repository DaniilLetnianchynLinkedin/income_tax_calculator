namespace TaxCalculatorApi.Contracts.Entities
{
    public class TaxBand
    {
        public int Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal? UpperLimit { get; set; }
        public decimal TaxRate { get; set; }
    }
}
