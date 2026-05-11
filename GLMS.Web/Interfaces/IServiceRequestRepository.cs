using GLMS.Web.Models;

namespace GLMS.Web.Interfaces;

public interface IServiceRequestRepository
{
    Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync();
    Task AddRequestAsync(ServiceRequest request);
    Task SaveAsync();
}
