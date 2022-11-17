using PetService.Domain.Converters;
using PetService.Domain.Entities;

namespace PetService.Domain.ApiModel
{
    public class PetApiModel : IConvertModel<PetApiModel, PetEntities>
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string Allergies { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string BloodGroup { get; set; }
        public int PetOwnerId { get; set; }

        public PetEntities Convert() =>
            new PetEntities
            {
                PetId = PetId,
                PetName = PetName,
                Species = Species,
                Gender = Gender,
                Allergies = Allergies,
                DateOfBirth = DateOfBirth,
                Age = Age,
                BloodGroup = BloodGroup,
                PetOwnerId = PetOwnerId
            };

    }
}


