using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;

public record OutgoingCreateTaxBandDTO(int Id) : IOutgoingControllerDTO;