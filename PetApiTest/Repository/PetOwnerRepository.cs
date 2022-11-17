using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetService.Domain.Repositories;



namespace PetApiTest.Repository
{
    public class PetOwnerRepositoryTest
    {
        private readonly IPetOwnerRepository _repo;



        public PetOwnerRepositoryTest()
        {



        }



        [Test]
        public void GetAllPetOwners()
        {
            var owners = _repo.GetAllPetOwners();
            Assert.True(owners.Count > 1, "The number of pet owners was not greater then 1 ");
        }
        [Test]
        public void GetOneOwner()
        {
            // Arrange
            var id = 1;



            // Act
            var pet = _repo.GetPetOwnerById(id);



            // Assert
            Assert.Equals(id, pet.PetOwnerId);
        }
    }
}