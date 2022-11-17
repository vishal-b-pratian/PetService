using PetService.Domain.ApiModel;

namespace PetService.Domain.Managers
{
    public interface IPetManager 
    {
        #region Sync Methods
        IEnumerable<PetApiModel> GetAllPets();
        PetApiModel GetPetById(int id);
        PetApiModel AddPet(PetApiModel petEntities);
        bool UpdatePet(PetApiModel petEntities);
        bool DeletePet(int id);
        #endregion

        #region Async Methods
        Task<IEnumerable<PetApiModel>> GetAllPetsAsync();
        Task<PetApiModel> GetPetByIdAsync(int id);
        Task<PetApiModel> AddPetAsync(PetApiModel petEntities);
        Task<bool> UpdatePetAsync(PetApiModel petEntities);
        Task<bool> DeletePetAsync(int id);

        #endregion

    }
}
