using Enterprise.Models.Requests;
using FluentValidation;

public class QuotationValidator : AbstractValidator<PostQuotationDTO>
{
    public QuotationValidator()
    {
        RuleFor(q => q.Units).Must( (u) => u > 0 ).WithMessage("Units must be greater than 0");
    }
}