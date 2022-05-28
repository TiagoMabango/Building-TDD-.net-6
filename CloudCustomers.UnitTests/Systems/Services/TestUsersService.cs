
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GerAllUsers_WhenCalled_InvokeHttpGetRequest() 
        {
            //Arrange
            var expectedResponde = UsersFixture.GetTestUsers();
            var handlerMock = 
                MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponde);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);
            //Act
            await sut.GetAllUsers();
            //Assert
            handlerMock
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>( req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>() 
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var expectedResponde = UsersFixture.GetTestUsers();
            var handlerMock =
                MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponde);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Should().BeOfType<List<User>>();
        }
    }
}
