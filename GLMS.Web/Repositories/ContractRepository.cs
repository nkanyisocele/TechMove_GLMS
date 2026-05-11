using GLMS.Web.Data;
using GLMS.Web.Interfaces;
using GLMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Repositories;

public class ContractRepository(ApplicationDbContext context) : IContractRepository
{
    public async Task<IEnumerable<Contract>> GetAllContractsAsync()
    {
        return await context.Contracts.Include(c => c.Client).ToListAsync();
    }

    public async Task<Contract?> GetContractByIdAsync(int id)
    {
        return await context.Contracts
            .Include(c => c.Client)
            .FirstOrDefaultAsync(m => m.ContractId == id);
    }

    public async Task AddContractAsync(Contract contract)
    {
        await context.Contracts.AddAsync(contract);
    }

    public async Task UpdateContractAsync(Contract contract)
    {
        context.Contracts.Update(contract);
    }

    public async Task DeleteContractAsync(int id)
    {
        var contract = await context.Contracts.FindAsync(id);
        if (contract != null) context.Contracts.Remove(contract);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
