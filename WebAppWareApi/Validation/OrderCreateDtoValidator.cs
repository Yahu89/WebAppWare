using FluentValidation;
using WebAppWare.Api.Dto;

namespace WebAppWare.Api.Validation
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.Document).NotEmpty().WithMessage("Numer dokumentu nie może być pusty")
                                .MinimumLength(2).WithMessage("Numer dokumentu musi składać się z co najmniej 2 znaków");

            RuleFor(x => x.CreationDate).NotEmpty().WithMessage("Pole daty nie może być puste");

            RuleFor(x => x.SupplierId).Must(val => val == (int)val && val >= 1).WithMessage("Podana wartość musi być liczbą całkowitą");

            RuleFor(x => x.Status).Must(val => val == (int)val && (val >= 1 && val <= 4))
                                    .WithMessage("Podana wartość musi być liczbą z przedziału od 1 do 4");
        }
    }
}
