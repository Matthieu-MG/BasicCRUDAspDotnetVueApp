using Enterprise.Models.Requests;
using FluentValidation;

public class SocietyValidator : AbstractValidator<PostSocietyDTO>
{
    public SocietyValidator(SocietyRepository societyRepository)
    {
        RuleFor(s => s.Name).NotEmpty();
        RuleFor(s => s.FullName).NotEmpty().MustAsync( async (name, _) =>
        {
            return await societyRepository.IsSocietyFullNameUnique(name ?? "");
        });
    }
}