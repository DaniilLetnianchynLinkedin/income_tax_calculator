using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;

namespace TaxCalculator.Contract.CQS.TaxBand.Commands;

[FeatureGate(Features.TaxCalculator)]
public record CreateTaxBandCommand(decimal LowerLimit, decimal? UpperLimit, decimal TaxRate) : IControllerServiceBusRequest<OutgoingCreateTaxBandDTO>;