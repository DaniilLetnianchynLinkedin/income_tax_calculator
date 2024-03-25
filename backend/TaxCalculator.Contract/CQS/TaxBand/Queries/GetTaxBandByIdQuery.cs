using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;

namespace TaxCalculator.Contract.CQS.TaxBand.Queries;

[FeatureGate(Features.TaxCalculator)]
public record GetTaxBandByIdQuery(int Id) : IServiceBusRequest<OutgoingGetTaxBandByIdDTO>;