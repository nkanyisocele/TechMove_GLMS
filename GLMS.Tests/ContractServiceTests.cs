using GLMS.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace GLMS.Tests;

// Using WebApplicationFactory to spin up an in-memory test instance of your real API running in program.cs
public class ContractIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContractIntegrationTests(WebApplicationFactory<Program> factory)
    {
        // Creates an isolated HttpClient mapped to the in-memory test API instance
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Contract_CreateThenRead_Lifecycle_MaintainsDataIntegrity()
    {
        // 1. Arrange: Define a test contract payload matching your API specifications
        var newContract = new Contract
        {
            ContractId = 999, 
            Status = "Active",
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(30)
        };

        // 2. Act: Part A - Send a POST request to add the contract to the system
        var postResponse = await _client.PostAsJsonAsync("api/contracts", newContract);

        // 3. Assert: Part A - Verify the creation endpoint returns 201 Created
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        // 4. Act: Part B - Instantly fetch all records using GET to confirm the database persistent write
        var getResponse = await _client.GetAsync("api/contracts");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var contracts = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Contract>>();

        // 5. Assert: Part B - Prove data integrity by ensuring the written object perfectly matches our intent
        Assert.NotNull(contracts);
        var savedContract = contracts.FirstOrDefault(c => c.ContractId == 999);

        Assert.NotNull(savedContract);
        Assert.Equal("Active", savedContract.Status);
    }
}

