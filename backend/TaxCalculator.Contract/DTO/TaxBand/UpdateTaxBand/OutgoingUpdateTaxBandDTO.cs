using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;

public record OutgoingUpdateTaxBandDTO(int Id) : IOutgoingControllerDTO;