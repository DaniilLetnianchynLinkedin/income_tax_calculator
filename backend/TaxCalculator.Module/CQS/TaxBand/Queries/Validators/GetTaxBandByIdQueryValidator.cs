using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Queries;

namespace TaxCalculator.Module.CQS.TaxBand.Queries.Validators;

public class GetTaxBandByIdQueryValidator : AbstractValidator<GetTaxBandByIdQuery>
{
    public GetTaxBandByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}