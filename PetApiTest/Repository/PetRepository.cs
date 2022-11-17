using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetService.Domain.Repositories;



namespace PetApiTest.Repository
{
    public class PetRepositoryTest
    {
        private readonly IPetRepository _repo;



        public PetRepositoryTest()
        {



        }



        [Test]
        public void GetAllPets()
        {
            var pets = _repo.GetAllPets();
            Assert.True(pets.Count > 1, "The number of pets was not greater then 1 ");
        }
        [Test]
        public void GetOnePet()
        {
            // Arrange
            var id = 1;



            // Act
            var pet = _repo.GetPetById(id);



            // Assert
            Assert.Equals(id, pet.PetId);
        }
    }
}