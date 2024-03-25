using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Commands;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Validators;

public class UpdateTaxBandCommandValidator : AbstractValidator<UpdateTaxBandCommand>
{
    public UpdateTaxBandCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}