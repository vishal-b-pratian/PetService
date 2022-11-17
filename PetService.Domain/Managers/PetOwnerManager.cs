using Microsoft.Extensions.Caching.Memory;
using PetService.Domain.ApiModel;
using PetService.Domain.Extensions;
using PetService.Domain.Repositories;

namespace PetService.Domain.Managers
{
    public partial class PetOwnerManager : IPetOwnerManager
    {
        private readonly IPetOwnerRepository _petOwnerRepository;
        private readonly IMemoryCache _cache;


        public PetOwnerManager(IPetOwnerRepository petOwnerRepository, IMemoryCache cache)
        {
            _petOwnerRepository = petOwnerRepository;
            _cache = cache;
        }

        public PetOwnerApiModel AddPetOwner(PetOwnerApiModel newOwner)
        {
            var petOwner = newOwner.Convert();

            petOwner = _petOwnerRepository.AddPetOwner(petOwner);
            newOwner.PetOwnerId = petOwner.PetOwnerId;
            return newOwner;
        }

        public async Task<PetOwnerApiModel> AddPetOwnerAsync(PetOwnerApiModel newOwner)
        {
            var petOwner = newOwner.Convert();

            petOwner = await _petOwnerRepository.AddPetOwnerAsync(petOwner);
            newOwner.PetOwnerId = petOwner.PetOwnerId;
            return newOwner;
        }

        public bool DeletePetOwner(int id) => _petOwnerRepository.DeletePetOwner(id);

        public async Task<bool> DeletePetOwnerAsync(int id)
        {
            return await _petOwnerRepository.DeletePetOwnerAsync(id);
        }

        public IEnumerable<PetOwnerApiModel> GetAllPetOwners()
        {
            var petOwners = _petOwnerRepository.GetAllPetOwners().ConvertAll();
            foreach (var owner in petOwners)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("PetOwner-", owner.PetOwnerId), owner, cacheEntryOptions);
            }

            return petOwners;
        }

        public async Task<IEnumerable<PetOwnerApiModel>> GetAllPetOwnersAsync()
        {
            var owners = (await _petOwnerRepository.GetAllPetOwnersAsync()).ConvertAll();
            foreach (var owner in owners)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("PetOwner-", owner.PetOwnerId), owner, cacheEntryOptions);
            }

            return owners;
        }

        public PetOwnerApiModel GetPetOwnerById(int id)
        {
            var petOwnerApiModelCached = _cache.Get<PetOwnerApiModel>(string.Concat("PetOwner-", id));

            if (petOwnerApiModelCached != null)
            {
                return petOwnerApiModelCached;
            }
            else
            {
                var owner = _petOwnerRepository.GetPetOwnerById(id);
                if (owner == null) return null;
                var petOwnerApiModel = owner.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Appointment-", petOwnerApiModel.PetOwnerId), petOwnerApiModel, cacheEntryOptions);

                return petOwnerApiModel;
            }
        }

        public async Task<PetOwnerApiModel> GetPetOwnerByIdAsync(int id)
        {
            var petOwnerApiModelCached = _cache.Get<PetOwnerApiModel>(string.Concat("PetOwner-", id));

            if (petOwnerApiModelCached != null)
            {
                return petOwnerApiModelCached;
            }
            else
            {
                var owner = await _petOwnerRepository.GetPetOwnerByIdAsync(id);
                if (owner == null) return null;
                var petOwnerApiModel = owner.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Appointment-", petOwnerApiModel.PetOwnerId), petOwnerApiModel, cacheEntryOptions);

                return petOwnerApiModel;
            }
        }

        public bool UpdatePetOwner(PetOwnerApiModel petOwner)
        {
            var owner = _petOwnerRepository.GetPetOwnerById(petOwner.PetOwnerId);

            if (owner is null) return false;
            owner.PetOwnerId = petOwner.PetOwnerId;
            owner.ParentName = petOwner.ParentName;
            owner.Email = petOwner.Email;
            owner.Mobile = petOwner.Mobile;
            owner.Location = petOwner.Location;
            owner.Pincode = petOwner.Pincode;
            owner.Gender = petOwner.Gender;
            return _petOwnerRepository.UpdatePetOwner(owner);
        }

        public async Task<bool> UpdatePetOwnerAsync(PetOwnerApiModel petOwner)
        {
            var owner = await _petOwnerRepository.GetPetOwnerByIdAsync(petOwner.PetOwnerId);

            if (owner is null) return false;
            owner.PetOwnerId = petOwner.PetOwnerId;
            owner.ParentName = petOwner.ParentName;
            owner.Email = petOwner.Email;
            owner.Mobile = petOwner.Mobile;
            owner.Location = petOwner.Location;
            owner.Pincode = petOwner.Pincode;
            owner.Gender = petOwner.Gender;
            return await _petOwnerRepository.UpdatePetOwnerAsync(owner);
        }
    }
}
