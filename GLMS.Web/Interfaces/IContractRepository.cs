using GLMS.Web.Models;

namespace GLMS.Web.Interfaces;

public interface IContractRepository
{
    Task<IEnumerable<Contract>> GetAllContractsAsync();
    Task<Contract?> GetContractByIdAsync(int id);
    Task AddContractAsync(Contract contract);
    Task UpdateContractAsync(Contract contract);
    Task DeleteContractAsync(int id);
    Task SaveAsync();
}
