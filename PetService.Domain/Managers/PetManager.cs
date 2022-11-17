using Microsoft.Extensions.Caching.Memory;
using PetService.Domain.ApiModel;
using PetService.Domain.Extensions;
using PetService.Domain.Repositories;

namespace PetService.Domain.Managers
{
    public partial class PetManager : IPetManager
    {
        private readonly IPetRepository _petRepository;
        private readonly IMemoryCache _cache;


        public PetManager(IPetRepository appointmentRepository, IMemoryCache cache)
        {
            _petRepository = appointmentRepository;
            _cache = cache;
        }

        public PetApiModel AddPet(PetApiModel newPet)
        {
            var pet = newPet.Convert();

            pet = _petRepository.AddPet(pet);
            newPet.PetId = pet.PetId;
            return newPet;
        }

        public async Task<PetApiModel> AddPetAsync(PetApiModel newPet)
        {
            var pet = newPet.Convert();

            pet = await _petRepository.AddPetAsync(pet);
            newPet.PetId = pet.PetId;
            return newPet;
        }

        public bool DeletePet(int id) => _petRepository.DeletePet(id);


        public async Task<bool> DeletePetAsync(int id)
        {
            return await _petRepository.DeletePetAsync(id);
        }

        public IEnumerable<PetApiModel> GetAllPets()
        {
            var pets = _petRepository.GetAllPets().ConvertAll();
            foreach (var pet in pets)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Pet-", pet.PetId), pet, cacheEntryOptions);
            }

            return pets;
        }

        public async Task<IEnumerable<PetApiModel>> GetAllPetsAsync()
        {
            var pets = (await _petRepository.GetAllPetsAsync()).ConvertAll();
            foreach (var pet in pets)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Pet-", pet.PetId), pet, cacheEntryOptions);
            }

            return pets;
        }

        public PetApiModel GetPetById(int id)
        {
            var petApiModelCached = _cache.Get<PetApiModel>(string.Concat("Pet-", id));

            if (petApiModelCached != null)
            {
                return petApiModelCached;
            }
            else
            {
                var pet = _petRepository.GetPetById(id);
                if (pet == null) return null;
                var petApiModel = pet.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Pet-", petApiModel.PetId), petApiModel, cacheEntryOptions);

                return petApiModel;
            }
        }

        public async Task<PetApiModel> GetPetByIdAsync(int id)
        {
            var petApiModelCached = _cache.Get<PetApiModel>(string.Concat("Album-", id));

            if (petApiModelCached != null)
            {
                return petApiModelCached;
            }
            else
            {
                var pet = await _petRepository.GetPetByIdAsync(id);
                if (pet == null) return null;
                var petApiModel = pet.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Pet-", petApiModel.PetId), petApiModel, cacheEntryOptions);

                return petApiModel;
            }
        }

        public bool UpdatePet(PetApiModel pet)
        {
            var pett = _petRepository.GetPetById(pet.PetId);

            if (pett is null) return false;
            pett.PetId = pet.PetId;
            pett.PetName = pet.PetName;
            pett.Species = pet.Species;
            pett.Gender = pet.Gender;
            pett.Allergies = pet.Allergies;
            pett.DateOfBirth = pet.DateOfBirth;
            pett.Age = pet.Age;
            pett.BloodGroup = pet.BloodGroup;
            pett.PetOwnerId = pet.PetOwnerId;
            return _petRepository.UpdatePet(pett);
        }

        public async Task<bool> UpdatePetAsync(PetApiModel pet)
        {
            var pett = await _petRepository.GetPetByIdAsync(pet.PetId);

            if (pett is null) return false;
            pett.PetId = pet.PetId;
            pett.PetName = pet.PetName;
            pett.Species = pet.Species;
            pett.Gender = pet.Gender;
            pett.Allergies = pet.Allergies;
            pett.DateOfBirth = pet.DateOfBirth;
            pett.Age = pet.Age;
            pett.BloodGroup = pet.BloodGroup;
            pett.PetOwnerId = pet.PetOwnerId;

            return await _petRepository.UpdatePetAsync(pett);
        }
    }
}
