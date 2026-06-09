using GLMS.API.Data;
using GLMS.API.Interfaces;
using GLMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS.API.Repositories;

public class ServiceRequestRepository(ApplicationDbContext context) : IServiceRequestRepository
{
    public async Task<IEnumerable<ServiceRequest>> GetAllRequestsAsync() =>
        await context.ServiceRequests.Include(s => s.Contract).ToListAsync();

    public async Task AddRequestAsync(ServiceRequest request) => await context.ServiceRequests.AddAsync(request);
    public async Task SaveAsync() => await context.SaveChangesAsync();
}
