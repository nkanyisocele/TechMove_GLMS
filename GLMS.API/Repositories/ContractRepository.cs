using GLMS.API.Data;
using GLMS.API.Interfaces;
using GLMS.API.Models;
using GLMS.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GLMS.API.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext context;

    public ContractRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

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
        await context.SaveChangesAsync();
    }

    public async Task UpdateContractAsync(Contract contract)
    {
        context.Contracts.Update(contract);
        await context.SaveChangesAsync();
    }

    public async Task DeleteContractAsync(int id)
    {
        var contract = await context.Contracts.FindAsync(id);
        if (contract != null)
        {
            context.Contracts.Remove(contract);
            await context.SaveChangesAsync();
        }
    }
}



