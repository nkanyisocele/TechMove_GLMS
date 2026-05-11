
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using GLMS.Web.Models;


namespace GLMS.Web.Data;

// Inheriting from IdentityDbContext satisfies the 'Identity' requirement
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    // Define your tables (DbSets)
    public DbSet<Client> Clients { get; set; }
    public DbSet<Models.Contract> Contracts { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Critical for Identity to work

       

        // 1. Precise Math for Currency (Rubric 5)
        builder.Entity<ServiceRequest>()
            .Property(s => s.CostUSD)
            .HasPrecision(18, 2);

        builder.Entity<ServiceRequest>()
            .Property(s => s.CostZAR)
            .HasPrecision(18, 2);

        // 2. Client Relationships (Rubric 1)
        builder.Entity<Client>()
            .HasMany(c => c.Contracts)
            .WithOne(con => con.Client)
            .HasForeignKey(con => con.ClientId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents deleting a client if they have active contracts

        // 3. Contract Status Default
        builder.Entity<Models.Contract>()
            .Property(c => c.Status)
            .HasDefaultValue("Draft");


    }

}

