
using PetService.Domain.ApiModel;
using PetService.Domain.Converters;
using System.ComponentModel.DataAnnotations;

namespace PetService.Domain.Entities
{
    public class PetOwnerEntities : IConvertModel<PetOwnerEntities, PetOwnerApiModel>
    {
        [Key]
        public int PetOwnerId { get; set; }
        public string ParentName { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Location { get; set; }
        public string Pincode { get; set; }
        public string Gender { get; set; }

        public PetOwnerApiModel Convert() =>
            new PetOwnerApiModel
            {
                PetOwnerId = PetOwnerId,
                ParentName = ParentName,
                Email = Email,
                Mobile = Mobile,
                Location = Location,
                Pincode = Pincode,
                Gender = Gender
            };
    }
}
