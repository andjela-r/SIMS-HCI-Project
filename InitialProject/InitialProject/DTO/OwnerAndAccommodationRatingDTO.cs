using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class OwnerAndAccommodationRatingDTO
    {
        public int Id { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerHospitality { get; set; }
        public string Comment { get; set; }
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public List<string> Pictures { get; set; }
        public bool IsRated { get; set; }

        public OwnerAndAccommodationRatingDTO(int cleanliness, int ownerHospitality, string comment, int ownerId, int accommodationId, List<string> pictures)
        {
            this.Cleanliness = cleanliness;
            this.OwnerHospitality = ownerHospitality;
            this.Comment = comment;
            this.OwnerId = ownerId;
            this.AccommodationId = accommodationId;
            this.Pictures = pictures;
        }

        public OwnerAndAccommodationRatingDTO() { }
    }
}
