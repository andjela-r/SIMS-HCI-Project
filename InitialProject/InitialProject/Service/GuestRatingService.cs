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
    internal class GuestRatingService
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

        public void CreateGuestRating(GuestRating guestRating)
        {
            GuestRating guestRatingModel = new GuestRating();
            guestRatingModel.Cleanliness = guestRating.Cleanliness;
            guestRatingModel.Obedience = guestRating.Obedience;
            guestRatingModel.Comment = guestRating.Comment;
            guestRatingModel.GuestId = guestRating.GuestId;

            guestRatingRepository.Save(guestRatingModel);
        }

        private static void RateAGuest()
        {
             AccommodationReservation accommodationReservation = new AccommodationReservation();
             if (!IsDeadlineOver(accommodationReservation))
             {
                 Console.WriteLine("You have not rated a guest yet.");
             } else Console.WriteLine("You have already rated a guest.");

            int cleanliness, obedience;
            GuestRatingService guestRatigService = new GuestRatingService();

            Console.WriteLine("Rate cleanliness 1-5: ");
            cleanliness = Convert.ToInt32(Console.ReadLine());

            while (cleanliness > 5 || cleanliness < 1)
            {
                Console.WriteLine("Invalid rating. Try again, 1-5: ");
                Console.WriteLine("Rate cleanliness 1-5: ");
                cleanliness = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Rate obedience 1-5: ");
            obedience = Convert.ToInt32(Console.ReadLine());

            while (obedience > 5 || obedience < 1)
            {
                Console.WriteLine("Invalid rating. Try again, 1-5: ");
                Console.WriteLine("Rate obedience 1-5: ");
                obedience = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Insert comment: ");
            string comment = Console.ReadLine();

            Console.WriteLine("Insert guest id: ");
            int guestId = Convert.ToInt32(Console.ReadLine());

            GuestRatingDTO guestRatingDTO = new GuestRatingDTO(
                cleanliness, obedience, comment, guestId);
            guestRatigService.CreateGuestRating(guestRatingDTO);
        }

        private static bool IsDeadlineOver(AccommodationReservation accommodationReservation)
        {
            var today = DateTime.Today;
            var leavingDay = accommodationReservation.EndDate;
            var difference = today - leavingDay;

            if (difference.Days > 5)
            {
                return true;
            }

            return false;
        }






    }
}
