using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Commands;

namespace TaxCalculator.Module.CQS.TaxBand.Commands.Validators;

public class CreateTaxBandCommandValidator : AbstractValidator<CreateTaxBandCommand>
{
    public CreateTaxBandCommandValidator()
    {
    }
}