using PetService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetService.Domain.Repositories
{
    public interface IPetRepository : IDisposable
    {
        #region Sync Methods
        List<PetEntities> GetAllPets();
        PetEntities GetPetById(int id);
        PetEntities AddPet(PetEntities petEntities);
        bool UpdatePet(PetEntities petEntities);
        bool DeletePet(int id);
        #endregion

        #region Async Methods
        Task<List<PetEntities>> GetAllPetsAsync();
        Task<PetEntities> GetPetByIdAsync(int id);
        Task<PetEntities> AddPetAsync(PetEntities petEntities);
        Task<bool> UpdatePetAsync(PetEntities petEntities);
        Task<bool> DeletePetAsync(int id);

        #endregion

    }
}
