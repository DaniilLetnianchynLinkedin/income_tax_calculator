using System.Collections.Generic;
using Core.Abstractions.DTO;

namespace TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;

public record OutgoingGetAllTaxBandsDTO(IEnumerable<OutgoingGetAllTaxBandsSingleTaxBandDTO> TaxBands) : IOutgoingControllerDTO;