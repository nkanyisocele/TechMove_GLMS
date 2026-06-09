using GLMS.API.Data;
using GLMS.API.Interfaces;
using GLMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS.API.Repositories;

public class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    public async Task<IEnumerable<Client>> GetAllClientsAsync() => await context.Clients.ToListAsync();
    public async Task<Client?> GetClientByIdAsync(int id) => await context.Clients.FindAsync(id);
    public async Task AddClientAsync(Client client) => await context.Clients.AddAsync(client);
    public async Task SaveAsync() => await context.SaveChangesAsync();
}
