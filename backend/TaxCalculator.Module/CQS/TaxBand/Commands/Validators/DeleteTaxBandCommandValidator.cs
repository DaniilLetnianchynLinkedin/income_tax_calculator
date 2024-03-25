using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Commands;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Validators;

public class DeleteTaxBandCommandValidator : AbstractValidator<DeleteTaxBandCommand>
{
    public DeleteTaxBandCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}