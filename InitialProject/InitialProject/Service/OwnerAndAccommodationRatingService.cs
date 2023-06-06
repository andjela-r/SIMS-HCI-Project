using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class OwnerAndAccommodationRatingService
    {
        OwnerAndAccommodationRatingRepository ownerAndAccommodationRatingRepository = new OwnerAndAccommodationRatingRepository();

        public void CreateOwnerAndAccommodationRatingService(OwnerAndAccommodationRatingDTO ownerAndAccommodationRatingDTO)
        {
            OwnerAndAccommodationRating ownerAndAccommodationRating = new OwnerAndAccommodationRating();
            ownerAndAccommodationRating.Cleanliness = ownerAndAccommodationRatingDTO.Cleanliness;
            ownerAndAccommodationRating.OwnerHospitality = ownerAndAccommodationRatingDTO.OwnerHospitality;
            ownerAndAccommodationRating.Comment = ownerAndAccommodationRatingDTO.Comment;
            ownerAndAccommodationRating.OwnerId = ownerAndAccommodationRatingDTO.OwnerId;
            ownerAndAccommodationRating.AccommodationId = ownerAndAccommodationRatingDTO.AccommodationId;
            ownerAndAccommodationRating.Pictures = ownerAndAccommodationRatingDTO.Pictures;
            ownerAndAccommodationRating.IsRated = ownerAndAccommodationRatingDTO.IsRated;

            ownerAndAccommodationRatingRepository.Save(ownerAndAccommodationRating);

        }
    }
}
