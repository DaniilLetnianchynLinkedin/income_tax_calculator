using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;

public record OutgoingDeleteTaxBandDTO(int Id) : IOutgoingControllerDTO;