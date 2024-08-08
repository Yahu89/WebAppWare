using FluentValidation;
using WebAppWare.Api.Dto;

namespace WebAppWare.Api.Validation;

public class WarehouseDtoValidator : AbstractValidator<WarehouseDto>
{
    public WarehouseDtoValidator()
    {
        RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Nazwa magazynu nie może być pusta")
                            .MinimumLength(2).WithMessage("Nazwa magazynu musi składać się z co najmniej dwóch znaków");
    }
}
