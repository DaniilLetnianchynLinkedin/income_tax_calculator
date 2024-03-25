using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;

public record OutgoingTaxCalculationDTO(decimal GrossAnnualSalary, decimal NetAnnualSalary, decimal AnnualTaxPaid) : IOutgoingControllerDTO
{
    public decimal GrossMonthlySalary => GrossAnnualSalary / 12;
    public decimal NetMonthlySalary => NetAnnualSalary / 12;
    public decimal MonthlyTaxPaid => AnnualTaxPaid / 12;
}