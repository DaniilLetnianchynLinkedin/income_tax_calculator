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
using TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;

namespace TaxCalculator.Module.Controllers;

[Route("api/[controller]")]
[ApiController]
[FeatureGate(Features.TaxCalculator)]
public class TaxCalculationController : ApiControllerBase
{
    public TaxCalculationController(IMapper mapper, IServiceBus serviceBus)
        : base(mapper, serviceBus)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(OutgoingTaxCalculationDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateTax([FromBody] IncomingTaxCalculationDTO taxCalculationDto, CancellationToken cancellationToken)
    {

        var command = Mapper.Map<CalculateTaxCommand>(taxCalculationDto);
        var result = await ServiceBus.SendAsync<CalculateTaxCommand, OutgoingTaxCalculationDTO>(command, cancellationToken);
        return Send(result);


        //var grossSalary = taxCalculationRequest.GrossSalary;
        //var taxBands = await _context.TaxBands.OrderBy(b => b.LowerLimit).ToListAsync();
        //var taxCalculationResult = new TaxCalculationResult { GrossAnnualSalary = grossSalary };
        //decimal totalTax = 0;

        //foreach (var band in taxBands)
        //{
        //    if (grossSalary > band.LowerLimit)
        //    {
        //        var taxableAmountInBand = band.UpperLimit.HasValue && grossSalary > band.UpperLimit ? band.UpperLimit.Value - band.LowerLimit : grossSalary - band.LowerLimit;
        //        var taxInBand = taxableAmountInBand * (band.TaxRate / 100);
        //        totalTax += taxInBand;
        //        if (band.UpperLimit.HasValue && grossSalary <= band.UpperLimit) break;
        //    }
        //}

        //taxCalculationResult.AnnualTaxPaid = totalTax;
        //taxCalculationResult.NetAnnualSalary = grossSalary - totalTax;

        //return Ok(taxCalculationResult);
    }
}