using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;

public record IncomingUpdateTaxBandDTO(decimal LowerLimit, decimal? UpperLimit, decimal TaxRate) : IIncomingControllerDTO;