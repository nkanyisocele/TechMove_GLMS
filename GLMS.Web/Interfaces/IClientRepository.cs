using GLMS.Web.Models;

namespace GLMS.Web.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(int id);
    Task AddClientAsync(Client client);
    Task SaveAsync();
}
