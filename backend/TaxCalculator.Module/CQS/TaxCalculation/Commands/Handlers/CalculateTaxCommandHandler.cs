using AutoMapper;
using Core.Abstractions.HandlerResults;
using Core.Handlers;
using Microsoft.AspNetCore.Http;
using TaxCalculator.Contract.CQS.TaxBand.Commands;
using TaxCalculator.Contract.DTO.TaxBand.DeleteTaxBand;
using TaxCalculator.Contract.DTO.TaxCalculation.CalculateTax;
using TaxCalculator.Module.Data;
using Core.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;


namespace TaxCalculator.Module.CQS.TaxCalculation.Commands.Handlers
{
    internal class CalculateTaxCommandHandler : LoggableRequestHandlerBase<CalculateTaxCommand, OutgoingTaxCalculationDTO>
    {
        private readonly TaxCalculatorDataContext TaxCalculatorDataContext;
        private readonly IMapper mapper;

        public CalculateTaxCommandHandler(
            TaxCalculatorDataContext TaxCalculatorDataContext,
            IMapper mapper,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, httpContextAccessor)
        {
            this.TaxCalculatorDataContext = TaxCalculatorDataContext;
            this.mapper = mapper;
        }

        public override async Task<IHandlerResult<OutgoingTaxCalculationDTO>> Handle(CalculateTaxCommand command, CancellationToken cancellationToken)
        {

            var grossSalary = command.GrossSalary;
            var taxBands = await TaxCalculatorDataContext.TaxBands.OrderBy(b => b.LowerLimit).ToListAsync();
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

            var result = new OutgoingTaxCalculationDTO(grossSalary, grossSalary - totalTax, totalTax);

            return Data(result);
        }
    }
}