using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;

public record IncomingCreateTaxBandDTO(decimal LowerLimit, decimal? UpperLimit, decimal TaxRate) : IIncomingControllerDTO;