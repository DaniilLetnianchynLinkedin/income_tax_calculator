using FluentValidation;
using TaxCalculator.Contract.CQS.TaxBand.Queries;

namespace TaxCalculator.Module.CQS.TaxBand.Queries.Validators;

public class GetAllTaxBandsQueryValidator : AbstractValidator<GetAllTaxBandsQuery>
{
    public GetAllTaxBandsQueryValidator()
    {
    }
}