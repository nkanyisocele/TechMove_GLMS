using GLMS.API.Models;

namespace GLMS.API.Interfaces;

public interface IServiceRequestRepository
{
    Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync();
    Task AddRequestAsync(ServiceRequest request);
    Task SaveAsync();
}
