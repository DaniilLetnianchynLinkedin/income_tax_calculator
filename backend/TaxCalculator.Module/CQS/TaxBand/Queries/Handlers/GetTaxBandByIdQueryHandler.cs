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
using TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;

namespace TaxCalculator.Module.CQS.TaxBand.Queries.Handlers;

public class GetTaxBandByIdQueryHandler : LoggableRequestHandlerBase<GetTaxBandByIdQuery, OutgoingGetTaxBandByIdDTO>
{
    private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
    private readonly IMapper mapper;

    public GetTaxBandByIdQueryHandler(
        TaxCalculatorDataContext TaxCalculatorDataContext,
        ILogger logger,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : base(logger, httpContextAccessor)
    {
        this.TaxCalculatorDataContext = TaxCalculatorDataContext;
        this.mapper = mapper;
    }

    public override async Task<IHandlerResult<OutgoingGetTaxBandByIdDTO>> Handle(GetTaxBandByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await TaxCalculatorDataContext.TaxBands
            .Where(x => x.Id == query.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (result == null)
        {
            Logger.Info($"TaxBand with id {query.Id} not found.", HttpContextAccessor.HttpContext?.TraceIdentifier);
            return NotFound<OutgoingGetTaxBandByIdDTO>(new Errors.TaxBands.NotFoundError());
        }

        Logger.Info($"TaxBand with id {result.Id} was received.", HttpContextAccessor.HttpContext?.TraceIdentifier);
        return Data(mapper.Map<OutgoingGetTaxBandByIdDTO>(result));
    }
}