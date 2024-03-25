namespace TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;

public record OutgoingGetAllTaxBandsSingleTaxBandDTO(int Id, decimal LowerLimit, decimal? UpperLimit, decimal TaxRate);