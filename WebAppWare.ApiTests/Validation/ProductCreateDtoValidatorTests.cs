using Xunit;
using WebAppWare.Api.Dto;
using FluentValidation.TestHelper;

namespace WebAppWare.Api.Validation.Tests
{
    public class ProductCreateDtoValidatorTests
    {
        [Fact()]
        public void ProductCreateDtoValidator_CorrectData_ShouldBeSucceeded()
        {
            // arrange
            var validator = new ProductCreateDtoValidator();

            ProductCreateDto product = new ProductCreateDto()
            {
                ItemCode = "Wyrób 1",
                Description = "Opis 1"
            };

            // act

            var result = validator.TestValidate(product);

            // assert 

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ProductCreateDtoValidator_WrongData_ShouldNotBeSucceeded()
        {
            // arrange
            var validator = new ProductCreateDtoValidator();

            ProductCreateDto product = new ProductCreateDto()
            {
                ItemCode = "",
                Description = ""
            };

            // act

            var result = validator.TestValidate(product);

            // assert 

            result.ShouldHaveAnyValidationError();
        }
    }
}