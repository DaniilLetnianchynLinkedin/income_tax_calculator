using AutoMapper;
using TaxCalculator.Module.Data.Entities;
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;
using TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;
using TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;
using TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;

namespace TaxCalculator.Module.Mapping.TaxCalculations;


public class TaxCalculationsMappingProfile : Profile
{
    public TaxCalculationsMappingProfile()
    {
        CreateMap<IncomingTaxCalculationDTO, CalculateTaxCommand>();
    }
}