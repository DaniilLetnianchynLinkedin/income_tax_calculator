namespace TaxCalculatorApi.Contracts.DTOs
{
    public class TaxCalculationResult
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary => GrossAnnualSalary / 12;
        public decimal NetAnnualSalary { get; set; }
        public decimal NetMonthlySalary => NetAnnualSalary / 12;
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyTaxPaid => AnnualTaxPaid / 12;
    }
}
