using GLMS.Web.Data;
using GLMS.Web.Interfaces;
using GLMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Repositories;

public class ServiceRequestRepository(ApplicationDbContext context) : IServiceRequestRepository
{
    public async Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync() =>
        await context.ServiceRequests.Include(s => s.Contract).ToListAsync();

    public async Task AddRequestAsync(ServiceRequest request) => await context.ServiceRequests.AddAsync(request);
    public async Task SaveAsync() => await context.SaveChangesAsync();
}
