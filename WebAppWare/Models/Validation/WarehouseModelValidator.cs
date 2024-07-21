using FluentValidation;

namespace WebAppWare.Models.Validation;

public class WarehouseModelValidator : AbstractValidator<WarehouseModel>
{
    public WarehouseModelValidator()
    {
        RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Pole Nazwa magazynu nie może być puste")
                            .MinimumLength(2).WithMessage("Pole Nazwa magazynu musi składać się z co najmniej 2 znaków");
    }
}
