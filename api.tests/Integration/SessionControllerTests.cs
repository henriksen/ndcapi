using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace api.tests.Integration
{
    public class SessionControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public SessionControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_ReturnsNewSessionId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/Sessions", null);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var contents = await response.Content.ReadAsStringAsync();
            contents.Should().MatchRegex(@"\d+");
        }
        [Fact]
        public async Task Get_FreshSessionHas0AsValue()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/Sessions", null);
            var sessionId = await response.Content.ReadAsStringAsync();
            response = await client.GetAsync($"/api/Sessions/{sessionId}");

            // Assert
            var contents = await response.Content.ReadAsStringAsync();
            contents.Should().Be("0");
        }
        [Fact]
        public async Task Put_PuttingAValueUpdatesSession()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/Sessions", null);
            var sessionId = await response.Content.ReadAsStringAsync();
            var content = new StringContent("42", Encoding.UTF8,
                                    "application/json");
            _ = await client.PutAsync($"/api/Sessions/{sessionId}",content);
            response = await client.GetAsync($"/api/Sessions/{sessionId}");

            // Assert
            var contents = await response.Content.ReadAsStringAsync();
            contents.Should().Be("42");
        }
    }
}
