using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace WebAppWare.Controllers.Tests
{
    public class SupplierControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public SupplierControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]

        public async Task Index_ShouldReturnViewWithRenderModel()
        {
            // arrange

            var client = _factory.CreateClient();

            // act

            var response = await client.GetAsync("/Supplier/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}