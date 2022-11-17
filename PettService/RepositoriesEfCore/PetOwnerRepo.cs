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
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private readonly PetServicesDbContext _context;
        public PetOwnerRepository(PetServicesDbContext context)
        {
            _context = context;
        }
        private bool PetOwnerExists(int id) => _context.Petowners.Any(x => x.PetOwnerId == id);
        public void Dispose() => _context.Dispose();
        public PetOwnerEntities AddPetOwner(PetOwnerEntities petOwner)
        {
            _context.Petowners.Add(petOwner);
            _context.SaveChanges();
            return petOwner;
        }

        public async Task<PetOwnerEntities> AddPetOwnerAsync(PetOwnerEntities petOwner)
        {
            _context.Petowners.Add(petOwner);
            await _context.SaveChangesAsync();
            return petOwner;
        }

        public bool DeletePetOwner(int id)
        {
            if (!PetOwnerExists(id))
                return false;
            var toRemove = _context.Petowners.Find(id);
            _context.Petowners.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeletePetOwnerAsync(int id)
        {
            if (!PetOwnerExists(id))
                return false;
            var toRemove = await _context.Petowners.FindAsync(id);
            _context.Petowners.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<PetOwnerEntities> GetAllPetOwners() => _context.Petowners.AsNoTracking().ToList();

        public async Task<List<PetOwnerEntities>> GetAllPetOwnersAsync()
        {
            return await _context.Petowners.AsNoTracking().ToListAsync();
        }

        public PetOwnerEntities GetPetOwnerById(int id)
        {
            var appointment = _context.Petowners.Find(id);
            return appointment;
        }

        public async Task<PetOwnerEntities> GetPetOwnerByIdAsync(int id)
        {
            return await _context.Petowners.FindAsync(id);
        }

        public bool UpdatePetOwner(PetOwnerEntities appointment)
        {
            if (!PetOwnerExists(appointment.PetOwnerId))
                return false;
            _context.Petowners.Update(appointment);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdatePetOwnerAsync(PetOwnerEntities appointment)
        {
            if (!PetOwnerExists(appointment.PetOwnerId))
                return false;
            _context.Petowners.Update(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
