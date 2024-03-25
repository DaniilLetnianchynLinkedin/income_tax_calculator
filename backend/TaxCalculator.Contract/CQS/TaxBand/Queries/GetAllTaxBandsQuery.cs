using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;

namespace TaxCalculator.Contract.CQS.TaxBand.Queries;

[FeatureGate(Features.TaxCalculator)]
public record GetAllTaxBandsQuery() : IControllerServiceBusRequest<OutgoingGetAllTaxBandsDTO>;