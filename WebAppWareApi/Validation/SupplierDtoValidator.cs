using FluentValidation;
using WebAppWare.Api.Dto;

namespace WebAppWare.Api.Validation;

public class SupplierDtoValidator : AbstractValidator<SupplierDto>
{
    public SupplierDtoValidator()
    {
        RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Nazwa dostawcy nie może być pusta")
                            .MinimumLength(2).WithMessage("Nazwa dostawcy musi składać się z co najmniej dwóch znaków");

        RuleFor(x => x.Email)
                            .NotEmpty().WithMessage("Email dostawcy nie może być pusty")
                            .EmailAddress().WithMessage("Wprowadź adres em-mail");
    }
}
