using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Commands;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Validators;

public class CalculateTaxCommandValidator : AbstractValidator<CalculateTaxCommand>
{
    public CalculateTaxCommandValidator()
    {
        RuleFor(x => x.GrossSalary).GreaterThan(0);
    }
}