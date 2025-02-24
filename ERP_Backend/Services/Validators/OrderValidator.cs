using Enterprise.Models.Requests;
using FluentValidation;

public class OrderValidator : AbstractValidator<PostOrderDTO>
{
    // TODO Add Dates Validator, Add FK Validation
    public OrderValidator()
    {
        RuleFor(q => q.Units).Must( (u) => u > 0 ).WithMessage("Units must be greater than 0");
    }
}