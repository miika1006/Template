using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Template.Core.Item;
using System.Text.Json;

namespace Template.Tests;

public class BasicIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private HttpClient _client;

    public BasicIntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    

    [Fact]
    public async Task Test_V1_GetItems()
    {
        // Act
        var result = await GetAndDeserialize("/v1/item");

        // Assert
        Assert.NotNull(result);

    }

    private async Task<List<Item>> GetAndDeserialize(string route)
    {
        var response = await _client.GetAsync(route);
        response.EnsureSuccessStatusCode();
        Stream result = await response.Content.ReadAsStreamAsync();
        var deserialized = await JsonSerializer.DeserializeAsync<List<Item>>(result);
        return deserialized!;
    }
}
