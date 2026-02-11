using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ObjectEntity> Objects 
        { get; set; } = null!;

        public DbSet<Machine> Machines
        { get; set; } = null!;

        public DbSet<ServiceRequest> ServiceRequests 
        { get; set; } = null!;
    }
}
