using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;
public record IncomingTaxCalculationDTO(long GrossSalary) : IIncomingControllerDTO;
