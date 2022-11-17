using Microsoft.EntityFrameworkCore;
using PetService.Domain.Entities;

namespace PetService.Data
{
    public class PetServicesDbContext : DbContext
    {

        public PetServicesDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PetEntities> Pets { get; set; }
        public DbSet<PetOwnerEntities> Petowners { get; set; }

    }
}