using PetService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetService.Domain.Repositories
{
    public interface IPetOwnerRepository : IDisposable
    {
        #region Sync Methods
        List<PetOwnerEntities> GetAllPetOwners();
        PetOwnerEntities GetPetOwnerById(int id);
        PetOwnerEntities AddPetOwner(PetOwnerEntities petOwnerEntities);
        bool UpdatePetOwner(PetOwnerEntities petOwnerEntities);
        bool DeletePetOwner(int id);

        #endregion

        #region Async Methods
        Task<List<PetOwnerEntities>> GetAllPetOwnersAsync();
        Task<PetOwnerEntities> GetPetOwnerByIdAsync(int id);
        Task<PetOwnerEntities> AddPetOwnerAsync(PetOwnerEntities petOwnerEntities);
        Task<bool> UpdatePetOwnerAsync(PetOwnerEntities petOwnerEntities);
        Task<bool> DeletePetOwnerAsync(int id);

        #endregion
    }
}
