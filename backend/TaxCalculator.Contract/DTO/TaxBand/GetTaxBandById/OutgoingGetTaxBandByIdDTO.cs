using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;

public record OutgoingGetTaxBandByIdDTO(int Id, decimal LowerLimit, decimal? UpperLimit, decimal TaxRate) : IOutgoingControllerDTO;