using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;

namespace TaxCalculator.Contract.CQS.TaxBand.Commands;

[FeatureGate(Features.TaxCalculator)]
public record DeleteTaxBandCommand(int Id) : IControllerServiceBusRequest<OutgoingDeleteTaxBandDTO>;