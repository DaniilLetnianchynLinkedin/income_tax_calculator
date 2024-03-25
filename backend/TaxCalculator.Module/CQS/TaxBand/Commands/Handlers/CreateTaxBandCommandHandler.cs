using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Core.Abstractions.HandlerResults;
using Core.Abstractions.Logging;
using Core.Handlers;
using TaxCalculator.Module.Data;
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;
using Core.Abstractions.DTO;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Handlers;

public class CreateTaxBandCommandHandler : LoggableRequestHandlerBase<CreateTaxBandCommand, OutgoingCreateTaxBandDTO>
{
    private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
    private readonly IMapper mapper;

    public CreateTaxBandCommandHandler(
        TaxCalculatorDataContext TaxCalculatorDataContext,
        ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
        : base(logger, httpContextAccessor)
    {
        this.TaxCalculatorDataContext = TaxCalculatorDataContext;
        this.mapper = mapper;
    }

    public override async Task<IHandlerResult<OutgoingCreateTaxBandDTO>> Handle(CreateTaxBandCommand command, CancellationToken cancellationToken)
    {
        var TaxBand = new Data.Entities.TaxBand
        {
            LowerLimit = command.LowerLimit,
            UpperLimit = command.UpperLimit,
            TaxRate = command.TaxRate
        };

        await TaxCalculatorDataContext.TaxBands.AddAsync(TaxBand, cancellationToken);
        await TaxCalculatorDataContext.SaveChangesAsync(cancellationToken);
        Logger.Info($"TaxBand with id {TaxBand.Id} was created.", HttpContextAccessor.HttpContext?.TraceIdentifier);

        return Data(mapper.Map<OutgoingCreateTaxBandDTO>(TaxBand));
    }
}