using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using Core.Controllers;
using TaxCalculator.Module.Errors;
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.CQS.TaxBand.Queries;
using TaxCalculator.Contract.DTO.TaxBand.CreateTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;
using TaxCalculator.Contract.DTO.TaxBand.GetAllTaxBands;
using TaxCalculator.Contract.DTO.TaxBand.GetTaxBandById;
using TaxCalculator.Contract.DTO.TaxBand.UpdateTaxBand;
using Microsoft.EntityFrameworkCore;

namespace TaxCalculator.Module.Controllers;

[Route("api/[controller]")]
[ApiController]
[FeatureGate(Features.TaxCalculator)]
public class TaxBandController : ApiControllerBase
{
    public TaxBandController(IMapper mapper, IServiceBus serviceBus)
        : base(mapper, serviceBus)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutgoingGetAllTaxBandsDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await ServiceBus.SendAsync<GetAllTaxBandsQuery, OutgoingGetAllTaxBandsDTO>(new GetAllTaxBandsQuery(), cancellationToken);
        return Send(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OutgoingGetTaxBandByIdDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await ServiceBus.SendAsync<GetTaxBandByIdQuery, OutgoingGetTaxBandByIdDTO>(new GetTaxBandByIdQuery(id), cancellationToken);
        return Send(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OutgoingCreateTaxBandDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] IncomingCreateTaxBandDTO TaxBandDto, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateTaxBandCommand>(TaxBandDto);
        var result = await ServiceBus.SendAsync<CreateTaxBandCommand, OutgoingCreateTaxBandDTO>(command, cancellationToken);
        return Send(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OutgoingUpdateTaxBandDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] IncomingUpdateTaxBandDTO TaxBandDto, CancellationToken cancellationToken)
    {
        var command = new UpdateTaxBandCommand(id, TaxBandDto.LowerLimit, TaxBandDto.UpperLimit, TaxBandDto.TaxRate);
        var result = await ServiceBus.SendAsync<UpdateTaxBandCommand, OutgoingUpdateTaxBandDTO>(command, cancellationToken);
        return Send(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(OutgoingDeleteTaxBandDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TaxBands.NotFoundError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await ServiceBus.SendAsync<DeleteTaxBandCommand, OutgoingDeleteTaxBandDTO>(new DeleteTaxBandCommand(id), cancellationToken);
        return Send(result);
    }    
}