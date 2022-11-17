using PetService.Domain.Entities;
using PetService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetService.Data;

namespace PetService.Data.RepositoriesEFCore
{
    public class PetRepository : IPetRepository
    {
        private readonly PetServicesDbContext _context;
        public PetRepository(PetServicesDbContext context)
        {
            _context = context;
        }
        private bool PetExists(int id) => _context.Pets.Any(x => x.PetId == id);
        public void Dispose() => _context.Dispose();
        public PetEntities AddPet(PetEntities petEntities)
        {
            _context.Pets.Add(petEntities);
            _context.SaveChanges();
            return petEntities;
        }

        public async Task<PetEntities> AddPetAsync(PetEntities petEntities)
        {
            _context.Pets.Add(petEntities);
            await _context.SaveChangesAsync();
            return petEntities;
        }

        public bool DeletePet(int id)
        {
            if (!PetExists(id))
                return false;
            var toRemove = _context.Pets.Find(id);
            _context.Pets.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeletePetAsync(int id)
        {
            if (!PetExists(id))
                return false;
            var toRemove = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<PetEntities> GetAllPets() => _context.Pets.AsNoTracking().ToList();

        public async Task<List<PetEntities>> GetAllPetsAsync()
        {
            return await _context.Pets.AsNoTracking().ToListAsync();
        }

        public PetEntities GetPetById(int id)
        {
            var petEntities = _context.Pets.Find(id);
            return petEntities;
        }

        public async Task<PetEntities> GetPetByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public bool UpdatePet(PetEntities petEntities)
        {
            if (!PetExists(petEntities.PetOwnerId))
                return false;
            _context.Pets.Update(petEntities);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdatePetAsync(PetEntities petEntities)
        {
            if (!PetExists(petEntities.PetId))
                return false;
            _context.Pets.Update(petEntities);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
