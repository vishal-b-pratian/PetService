using PetService.Domain.Converters;
using PetService.Domain.Entities;

namespace PetService.Domain.ApiModel
{
    public class PetOwnerApiModel :IConvertModel<PetOwnerApiModel, PetOwnerEntities>
    {
        public int PetOwnerId { get; set; }
        public string ParentName { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Location { get; set; }
        public string Pincode { get; set; }
        public string Gender { get; set; }

        public PetOwnerEntities Convert() =>
           new PetOwnerEntities
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
