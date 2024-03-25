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
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Handlers;

public class DeleteTaxBandCommandHandler : LoggableRequestHandlerBase<DeleteTaxBandCommand, OutgoingDeleteTaxBandDTO>
{
    private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
    private readonly IMapper mapper;

    public DeleteTaxBandCommandHandler(
        TaxCalculatorDataContext TaxCalculatorDataContext,
        IMapper mapper,
        ILogger logger,
        IHttpContextAccessor httpContextAccessor)
        : base(logger, httpContextAccessor)
    {
        this.TaxCalculatorDataContext = TaxCalculatorDataContext;
        this.mapper = mapper;
    }

    public override async Task<IHandlerResult<OutgoingDeleteTaxBandDTO>> Handle(DeleteTaxBandCommand command, CancellationToken cancellationToken)
    {
        var TaxBand = await TaxCalculatorDataContext.TaxBands
            .Where(x => x.Id == command.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (TaxBand == null)
        {
            Logger.Info($"TaxBand with id {command.Id} not found.", HttpContextAccessor.HttpContext?.TraceIdentifier);
            return BadRequest<OutgoingDeleteTaxBandDTO>(new Errors.TaxBands.NotFoundError());
        }
        
        TaxCalculatorDataContext.TaxBands.Remove(TaxBand);

        await TaxCalculatorDataContext.SaveChangesAsync(cancellationToken);

        Logger.Info($"TaxBand with id {TaxBand.Id} was deleted.", HttpContextAccessor.HttpContext?.TraceIdentifier);
        return Data(mapper.Map<OutgoingDeleteTaxBandDTO>(TaxBand));
    }
}