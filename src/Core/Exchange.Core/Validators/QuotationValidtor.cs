using FluentValidation;
using Exchange.Core.Contracts.Quotations;

namespace Exchange.Core.Validators
{
    /// <summary>
    /// Quotation Validator 
    /// </summary>
    public class QuotationValidator : AbstractValidator<Quotation>
    {
        /// <summary>
        /// Quotation Validator
        /// </summary>
        public QuotationValidator()
        {
            RuleFor(t => t.CurrencyCode).IsInEnum();
            RuleFor(t => t.AmountToBuy).NotNull()
                .GreaterThan((ulong) 0.0)
                .LessThanOrEqualTo(ulong.MaxValue);
        }
    }
}