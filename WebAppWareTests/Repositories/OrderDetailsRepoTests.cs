using Xunit;
using WebAppWare.Models;
using FluentAssertions;

namespace WebAppWare.Repositories.Tests
{
    public class OrderDetailsRepoTests
    {
        [Fact()]
        public void IsDataCorrect__WithFulfilledList_ShouldBeTrue()
        {
            // arange

            List<OrderDetailsModel> collection = new List<OrderDetailsModel>()
            {
                new OrderDetailsModel()
                {
                    ProductId = 1,
                    Quantity = 10
                },

                new OrderDetailsModel()
                {
                    ProductId = 2,
                    Quantity = 20
                },
            };

            OrderDetailsRepo repo = new OrderDetailsRepo(
                                new Database.WarehouseBaseContext(
                                new Microsoft.EntityFrameworkCore.DbContextOptions<Database.WarehouseBaseContext>()));

            // act

            var isDataCorrect = repo.IsDataCorrect(collection);

            // assert

            isDataCorrect.Should().BeTrue();


        }

        [Fact()]
        public void IsDataCorrect__WithListEmpty_ShouldBeFalse()
        {
            // arange

            List<OrderDetailsModel> collection = new List<OrderDetailsModel>();

            OrderDetailsRepo repo = new OrderDetailsRepo(
                                new Database.WarehouseBaseContext(
                                new Microsoft.EntityFrameworkCore.DbContextOptions<Database.WarehouseBaseContext>()));

            // act

            var isDataCorrect = repo.IsDataCorrect(collection);

            // assert

            isDataCorrect.Should().BeFalse();


        }
    }
}