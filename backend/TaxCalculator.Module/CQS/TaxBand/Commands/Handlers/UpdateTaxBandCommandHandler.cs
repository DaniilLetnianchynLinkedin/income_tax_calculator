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
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Handlers;

public class UpdateTaxBandCommandHandler : LoggableRequestHandlerBase<UpdateTaxBandCommand, OutgoingUpdateTaxBandDTO>
{
    private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
    private readonly IMapper mapper;

    public UpdateTaxBandCommandHandler(
        TaxCalculatorDataContext TaxCalculatorDataContext,
        ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
        : base(logger, httpContextAccessor)
    {
        this.TaxCalculatorDataContext = TaxCalculatorDataContext;
        this.mapper = mapper;
    }

    public override async Task<IHandlerResult<OutgoingUpdateTaxBandDTO>> Handle(UpdateTaxBandCommand command, CancellationToken cancellationToken)
    {
        var TaxBand = await TaxCalculatorDataContext.TaxBands
            .Where(x => x.Id == command.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (TaxBand == null)
        {
            Logger.Info($"TaxBand with id {command.Id} not found.", HttpContextAccessor.HttpContext?.TraceIdentifier);
            return NotFound<OutgoingUpdateTaxBandDTO>(new Errors.TaxBands.NotFoundError());
        }
        
        TaxBand.LowerLimit = command.LowerLimit;
        TaxBand.UpperLimit = command.UpperLimit;
        TaxBand.TaxRate = command.TaxRate;

        await TaxCalculatorDataContext.SaveChangesAsync(cancellationToken);
        Logger.Info($"TaxBand with id {command.Id} was updated.", HttpContextAccessor.HttpContext?.TraceIdentifier);

        return Data(mapper.Map<OutgoingUpdateTaxBandDTO>(TaxBand));
    }
}