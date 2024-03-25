using Core.Abstractions.Errors;
using Core.Errors;
using TaxCalculator.Module.Data.Entities;

namespace TaxCalculator.Module.Errors;

public abstract class TaxBands : BaseEntityErrors<TaxBand>
{
    public enum TaxBandConflictType
    {
        TaxBandCodeAlreadyExists,
    }

    public interface ITaxBandConflictError : IError
    {
        public TaxBandConflictType TaxBandConflictType { get; }
    }
}