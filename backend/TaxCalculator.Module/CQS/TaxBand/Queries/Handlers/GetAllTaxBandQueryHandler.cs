using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Core.Abstractions.HandlerResults;
using Core.Abstractions.Logging;
using Core.Handlers;
using TaxCalculator.Module.Data;
using TaxCalculator.Contract.CQS.TaxBand.Queries;
using TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;

namespace TaxCalculator.Module.CQS.TaxBand.Queries.Handlers;

public class GetAllTaxBandsQueryHandler : LoggableRequestHandlerBase<GetAllTaxBandsQuery, OutgoingGetAllTaxBandsDTO>
{
    private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
    private readonly IMapper mapper;
    
    public GetAllTaxBandsQueryHandler(
        ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        TaxCalculatorDataContext TaxCalculatorDataContext) 
        : base(logger, httpContextAccessor)
    {
        this.mapper = mapper;
        this.TaxCalculatorDataContext = TaxCalculatorDataContext;
    }

    public override async Task<IHandlerResult<OutgoingGetAllTaxBandsDTO>> Handle(GetAllTaxBandsQuery query, CancellationToken cancellationToken)
    {
        var result = await TaxCalculatorDataContext.TaxBands
            .ToListAsync(cancellationToken);
        
        Logger.Info("TaxBands with ids " + $"[{string.Join("], [", result.Select(m => m.Id).ToList())}] were received.", HttpContextAccessor.HttpContext?.TraceIdentifier);
        return Data(new OutgoingGetAllTaxBandsDTO(result.Select(x => mapper.Map<OutgoingGetAllTaxBandsSingleTaxBandDTO>(x))));
    }
}