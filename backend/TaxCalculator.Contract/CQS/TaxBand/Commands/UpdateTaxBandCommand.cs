using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;

namespace TaxCalculator.Contract.CQS.TaxBand.Commands;

[FeatureGate(Features.TaxCalculator)]
public record UpdateTaxBandCommand(int Id, decimal LowerLimit, decimal? UpperLimit, decimal TaxRate) : IControllerServiceBusRequest<OutgoingUpdateTaxBandDTO>;