using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaxCalculatorApi.Contracts.DTOs;

namespace TaxCalculatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaxCalculatorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CalculateTax")]
        public async Task<ActionResult<TaxCalculationResult>> CalculateTax(TaxCalculationRequest taxCalculationRequest)
        {
            var grossSalary = taxCalculationRequest.GrossSalary;
            var taxBands = await _context.TaxBands.OrderBy(b => b.LowerLimit).ToListAsync();
            var taxCalculationResult = new TaxCalculationResult { GrossAnnualSalary = grossSalary };
            decimal totalTax = 0;

            foreach (var band in taxBands)
            {
                if (grossSalary > band.LowerLimit)
                {
                    var taxableAmountInBand = band.UpperLimit.HasValue && grossSalary > band.UpperLimit ? band.UpperLimit.Value - band.LowerLimit : grossSalary - band.LowerLimit;
                    var taxInBand = taxableAmountInBand * (band.TaxRate / 100);
                    totalTax += taxInBand;
                    if (band.UpperLimit.HasValue && grossSalary <= band.UpperLimit) break;
                }
            }

            taxCalculationResult.AnnualTaxPaid = totalTax;
            taxCalculationResult.NetAnnualSalary = grossSalary - totalTax;

            return Ok(taxCalculationResult);
        }
    }
}
