using System;
using Xunit;
using Vintri.BeerInfo.API;
using Vintri.BeerInfo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net;

namespace Vintri.BeerInfo.Tests
{
    public class BeersControllerTest 
    {
        BeersController _controller;
        private readonly HttpClient _client;
        public BeersControllerTest()
        {
            //Arrange
            _controller = new BeersController();
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
                
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetBeers_ReturnsOKResult(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/Beers?beerName=Buzz");
            //Act
            var response = await _client.SendAsync(request);
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Theory]
        [InlineData("GET")]
        public async Task GetBeers_UnknownNamePassed_ReturnsNotFoundResult(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/Beers?beerName=9999");
            //Act
            var response = await _client.SendAsync(request);
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }


    }
}
