using Enterprise.Models.Requests;
using FluentValidation;

namespace Enterprise.API.Validators;

public class ProductValidator : AbstractValidator<PostProductDTO>
{
    public ProductValidator(ProductRepository productRepository)
    {
        RuleFor(p => p.Name).NotEmpty().MustAsync(async (name, _) =>
        {
            return await productRepository.IsProductNameUnique(name);
        }).WithMessage("Product Name must be Unique");
        
        RuleFor(p => p.StandardPrice).Must( (price) => price >= 0 )
        .WithMessage("Price must be greater or equal to 0");
    }
}