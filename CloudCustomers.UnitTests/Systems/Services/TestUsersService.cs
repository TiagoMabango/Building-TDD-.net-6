
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UsersAPI.cofig;
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
            var endpoint = "https://example.com/users";
            var config = Options.Create(

                new UsersApiOptions
                {
                    Endpoint = endpoint
                }

               );

            var sut = new UsersService(httpClient, config);
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
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //Arrange
           
            var handlerMock =
                MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(

                new UsersApiOptions
                {
                    Endpoint = endpoint
                }

               );

            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponde = UsersFixture.GetTestUsers();
            var handlerMock =
                MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponde);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(

                new UsersApiOptions
                {
                    Endpoint = endpoint
                }

               );

            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(expectedResponde.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponde = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock =
                MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponde);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions{
                    Endpoint = endpoint
                });
            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);
            //Assert
            handlerMock
               .Protected()
               .Verify("SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(
               req => req.Method == HttpMethod.Get
               && req.RequestUri == uri),
               ItExpr.IsAny<CancellationToken>()
               );
        }
    }
}
