using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;
using TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;

namespace TaxCalculator.Contract.CQS.TaxBand.Commands;

[FeatureGate(Features.TaxCalculator)]
public record CalculateTaxCommand(long GrossSalary) : IControllerServiceBusRequest<OutgoingTaxCalculationDTO>;