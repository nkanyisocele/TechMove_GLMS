using GLMS.Web.Models;

namespace GLMS.Web.Interfaces;

public interface IContractService
{
    Task<bool> IsContractActiveAsync(int contractId);
    Task<string> GetContractStatusAsync(int contractId);
    // This will help with the "Automated Status Tracking" mentioned in the docs
    Task UpdateContractStatusesAsync();
}

