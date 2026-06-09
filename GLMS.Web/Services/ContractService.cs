using System.Net.Http.Json;
using GLMS.API.Models;

namespace GLMS.Web.Services;

public class ContractService : IContractService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ContractService> _logger;
    private API.Interfaces.IContractRepository @object;

    public ContractService(HttpClient httpClient, ILogger<ContractService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public ContractService(API.Interfaces.IContractRepository @object)
    {
        this.@object = @object;
    }

    // HTTP replacement for fetching all contracts
    public async Task<IEnumerable<Contract>> GetAllContractsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/contracts");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Contract>>() ?? Array.Empty<Contract>();
            }
            return Array.Empty<Contract>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to contact GLMS.API backend.");
            return Array.Empty<Contract>(); // Safe failover grace
        }
    }

    // HTTP replacement for fetching a single contract by ID
    public async Task<Contract?> GetContractByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Contract>($"api/contracts/{id}");
        }
        catch
        {
            return null;
        }
    }

    // HTTP replacement for your original workflow validation check
    public async Task<bool> IsContractActiveAsync(int contractId)
    {
        var contract = await GetContractByIdAsync(contractId);
        if (contract == null) return false;

        return contract.Status == "Active" &&
               contract.StartDate <= DateTime.Now &&
               contract.EndDate >= DateTime.Now;
    }

    // HTTP replacement for checking specific statuses
    public async Task<string> GetContractStatusAsync(int contractId)
    {
        var contract = await GetContractByIdAsync(contractId);
        return contract?.Status ?? "NotFound";
    }

    public async Task<bool> CreateContractAsync(Contract contract)
    {
        var response = await _httpClient.PostAsJsonAsync("api/contracts", contract);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/contracts/{id}/status", status);
        return response.IsSuccessStatusCode;
    }

}

