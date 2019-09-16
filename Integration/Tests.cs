using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using AccountOwnerServer;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Entities.Models;

namespace IntegrationTests
{
    public class Tests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public Tests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllOwners_ReturnsOkResponse()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/owner");
            response.EnsureSuccessStatusCode();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllOwners_ReturnsAListOfOwners()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/owner");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var owner = JsonConvert.DeserializeObject<List<Owner>>(responseString);

            //Assert   
            Assert.NotEmpty(owner);
        }
    }
}
