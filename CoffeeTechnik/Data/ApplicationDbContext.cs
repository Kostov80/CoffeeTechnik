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

        // Таблица за обекти
        public DbSet<ObjectEntity> Objects { get; set; } = null!;

        // Таблица за стандартни машини
        public DbSet<Machine> Machines { get; set; } = null!;

        // Таблица за сервизни заявки
        public DbSet<ServiceRequest> ServiceRequests { get; set; } = null!;

        // Таблица за кафе машини
        public DbSet<CoffeeMachine> CoffeeMachines { get; set; } = null!;
    }
}