using FluentValidation;
using WebAppWare.Api.Dto;

namespace WebAppWare.Api.Validation;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(x => x.ItemCode).NotEmpty().WithMessage("Kod produktu nie może być pusty")
                                .MinimumLength(2).WithMessage("Kod produktu musi składać się z co najmniej 2 znaków");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Opis produktu nie może być pusty")
                                    .MinimumLength(3).WithMessage("Opis produktu musi składać się z co najmniej 3 znaków");
    }
}
