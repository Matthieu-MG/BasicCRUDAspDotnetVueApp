using Enterprise.Models.Requests;
using FluentValidation;

namespace Enterprise.API.Validators;

public class EmployeeValidator : AbstractValidator<PostEmployeeDTO>
{
    public EmployeeValidator(EmployeeRepository employeeRepository)
    {
        RuleFor(em => em.Email).NotEmpty().EmailAddress().WithMessage("Must be a valid email address.")
        .MustAsync(async (email, _) =>
        {
            return await employeeRepository.IsEmailUnique(email);
        }).WithMessage("The Email Address Must Be Unique.");

        RuleFor(em => em.Name).NotEmpty().WithMessage("Name Cannot be Empty.");
        RuleFor(em => em.Surname).NotEmpty().WithMessage("Surname Cannot be Empty.");
        RuleFor(em => em.DateOfBirth).Must( (date) =>
        {
            int currentYear = DateTime.Now.Year;
            return currentYear - date.Year > 17;
        }).WithMessage("Employee must be 18 or above");
    }
}