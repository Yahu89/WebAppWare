using FluentValidation;
using WebAppWare.Models;

namespace WebAppWareApi.Validation;

public class WarehouseModelValidator : AbstractValidator<WarehouseModel>
{
    public WarehouseModelValidator()
    {
        RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Nazwa magazynu nie może być pusta")
                            .MinimumLength(2).WithMessage("Nazwa magazynu musi składać się z co najmniej dwóch znaków");
    }
}
