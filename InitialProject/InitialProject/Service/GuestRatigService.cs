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
    internal class GuestRatigService
    {
        GuestRatingRepository guestRatingRepository = new GuestRatingRepository();

        public void CreateGuestRating(GuestRatingDTO guestRatingDTO)
         {
            GuestRating guestRating = new GuestRating();
            guestRating.Cleanliness = guestRatingDTO.Cleanliness;
            guestRating.Obedience = guestRatingDTO.Obedience;   
            guestRating.Comment = guestRatingDTO.Comment;
            guestRating.GuestId = guestRatingDTO.GuestId;

            guestRatingRepository.Save(guestRating);
        }




    }
}
