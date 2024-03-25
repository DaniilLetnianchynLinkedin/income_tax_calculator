using AutoMapper;
using TaxCalculator.Module.Data.Entities;
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;
using TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;
using TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;

namespace TaxCalculator.Module.Mapping.TaxBands;

public class TaxBandsMappingProfile : Profile
{
    public TaxBandsMappingProfile()
    {
        CreateMap<TaxBand, OutgoingGetAllTaxBandsSingleTaxBandDTO>();
        CreateMap<TaxBand, OutgoingGetTaxBandByIdDTO>();
        CreateMap<TaxBand, OutgoingCreateTaxBandDTO>();
        CreateMap<IncomingCreateTaxBandDTO, CalculateTaxCommand>();
        CreateMap<TaxBand, OutgoingUpdateTaxBandDTO>();
        CreateMap<TaxBand, OutgoingDeleteTaxBandDTO>();
    }
}