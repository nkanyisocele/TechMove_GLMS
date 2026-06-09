using GLMS.API.Models; // Reference your shared or API models
using GLMS.API.Interfaces;

namespace GLMS.Web.Services;

public interface IContractService
{
    Task<IEnumerable<Contract>> GetAllContractsAsync();
    Task<Contract?> GetContractByIdAsync(int id);
    Task<bool> CreateContractAsync(Contract contract);
    Task<bool> UpdateStatusAsync(int id, string status);
}

