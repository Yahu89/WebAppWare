using Xunit;
using AutoMapper;
using FluentAssertions;
using WebAppWare.Models;

namespace WebAppWare.Api.MappingProfile.Tests
{
    public class ProductMappingProfileTests
    {
        [Fact()]
        public void ProductMappingProfile_ShouldMapProductToProductModel()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ProductMappingProfile>());
            var mapper = configuration.CreateMapper();

            ProductModel product = new ProductModel()
            {
                Id = 1,
                ItemCode = "42014D",
                Description = "Opis testowy",
                ImageId = 4
            };

            // act

            var result = mapper.Map<ProductModel>(product);

            // assert

            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.ItemCode.Should().Be(product.ItemCode);
            result.Description.Should().Be(product.Description);
            result.ImageId.Should().Be(product.ImageId);
        }
    }
}