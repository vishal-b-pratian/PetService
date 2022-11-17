using PetService.Domain.ApiModel;
using PetService.Domain.Converters;
using System.ComponentModel.DataAnnotations;

namespace PetService.Domain.Entities
{
    public class PetEntities : IConvertModel<PetEntities, PetApiModel>
    {
        [Key]
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string Allergies { get; set; }
        public String DateOfBirth { get; set; }
        public int Age { get; set; }
        public string BloodGroup { get; set; }
        public int PetOwnerId { get; set; }

        public PetApiModel Convert() =>
           new PetApiModel
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
