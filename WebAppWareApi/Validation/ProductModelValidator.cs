using FluentValidation;
using WebAppWare.Models;

namespace WebAppWare.Api.Validation;

public class ProductModelValidator : AbstractValidator<ProductModel>
{
    public ProductModelValidator()
    {
        RuleFor(x => x.ItemCode)
                    .NotEmpty().WithMessage("Kod produktu nie może być pusty")
                    .MinimumLength(3).WithMessage("Kod produktu musi składać się z co najmniej 3 znaków");

        RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Opis produktu nie może być pusty");
    }
}
