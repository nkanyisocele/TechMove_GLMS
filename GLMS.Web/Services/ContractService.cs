using GLMS.Web.Interfaces;
using GLMS.Web.Models;

namespace GLMS.Web.Services;

public class ContractService(IContractRepository contractRepo) : IContractService
{
    public async Task<bool> IsContractActiveAsync(int contractId)
    {
        var contract = await contractRepo.GetContractByIdAsync(contractId);

        // Robust Validation: Check if exists, is marked Active, and date hasn't passed
        if (contract == null) return false;

        return contract.Status == "Active" &&
               contract.StartDate <= DateTime.Now &&
               contract.EndDate >= DateTime.Now;
    }

    public async Task<string> GetContractStatusAsync(int contractId)
    {
        var contract = await contractRepo.GetContractByIdAsync(contractId);
        return contract?.Status ?? "NotFound";
    }

    public async Task UpdateContractStatusesAsync()
    {
        var contracts = await contractRepo.GetAllContractsAsync();
        foreach (var contract in contracts)
        {
            if (contract.EndDate < DateTime.Now && contract.Status == "Active")
            {
                contract.Status = "Expired";
                await contractRepo.UpdateContractAsync(contract);
            }
        }
        await contractRepo.SaveAsync();
    }
}

