using Xunit;
using WebAppWare.Repositories.Interfaces;
using Moq;
using WebAppWare.Models;
using FluentAssertions;

namespace WebAppWare.Repositories.Tests
{
    public class UserAuthenticationTests
    {
        [Fact()]
        public async Task Login_CorrectUser_ShouldBeTrue()
        {
            // arrange

            LoginModelDto loginModel = new LoginModelDto()
            {
                UserName = "purchaseUser",
                Password = "Purchase01_"
            };

            var userAuthenticationMock = new Mock<IUserAuthentication>();
            userAuthenticationMock.Setup(x => x.Login(loginModel)).ReturnsAsync(true);
            var userAuth = userAuthenticationMock.Object;
            
            // act

            var result = await userAuth.Login(loginModel);

            // assert

            result.Should().Be(true);
        }

        [Fact()]
        public async Task Login_IncorrectUser_ShouldBeFalse()
        {
            // arrange

            LoginModelDto loginModel = new LoginModelDto()
            {
                UserName = "engUser",
                Password = "engPurchase01_"
            };

            var userAuthenticationMock = new Mock<IUserAuthentication>();
            userAuthenticationMock.Setup(x => x.Login(loginModel)).ReturnsAsync(false);
            var userAuth = userAuthenticationMock.Object;

            // act

            var result = await userAuth.Login(loginModel);

            // assert

            result.Should().Be(false);
        }
    }
}