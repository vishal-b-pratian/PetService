using PetService.Domain.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetService.Domain.Managers
{
    public interface IPetOwnerManager 
    {
        #region Sync Methods
        IEnumerable<PetOwnerApiModel> GetAllPetOwners();
        PetOwnerApiModel GetPetOwnerById(int id);
        PetOwnerApiModel AddPetOwner(PetOwnerApiModel petOwnerEntities);
        bool UpdatePetOwner(PetOwnerApiModel petOwnerEntities);
        bool DeletePetOwner(int id);

        #endregion

        #region Async Methods
        Task<IEnumerable<PetOwnerApiModel>> GetAllPetOwnersAsync();
        Task<PetOwnerApiModel> GetPetOwnerByIdAsync(int id);
        Task<PetOwnerApiModel> AddPetOwnerAsync(PetOwnerApiModel petOwnerEntities);
        Task<bool> UpdatePetOwnerAsync(PetOwnerApiModel petOwnerEntities);
        Task<bool> DeletePetOwnerAsync(int id);

        #endregion
    }
}
