using FluentValidation;

namespace WebAppWare.Models.Validation;

public class ProductModelValidator : AbstractValidator<ProductModel>
{
    public ProductModelValidator()
    {
        RuleFor(x => x.ItemCode)
                                .NotEmpty().WithMessage("Pole Indeks nie może być puste")
                                .MinimumLength(3).WithMessage("Pole Indeks musi składać się z co najmniej 3 znaków");

        RuleFor(c => c.Description)
								.NotEmpty().WithMessage("Pole Opis nie może być puste")
								.MinimumLength(3).WithMessage("Pole Opis musi składać się z co najmniej 3 znaków");
	}
}
