using FluentValidation;

namespace WebAppWare.Models.Validation;

public class SupplierModelValidator : AbstractValidator<SupplierModel>
{
    public SupplierModelValidator()
    {
        RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Pole Nazwa dostawcy nie może być puste")
                            .MinimumLength(1).WithMessage("Pole Nazwa dostawcy musi składać się z co najmniej 3 znaków");

        RuleFor(x => x.Email)
                            .NotEmpty().WithMessage("Pole Adres e-mail nie może być puste")
                            .EmailAddress().WithMessage("Pole musi być adresem e-mail");
	}
}
