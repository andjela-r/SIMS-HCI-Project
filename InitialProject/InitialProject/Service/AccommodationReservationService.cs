using System.DirectoryServices;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class AccommodationReservationService
    {
        AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();

        public void CreateAccommodationReservation(AccommodationReservationDTO accommodationReservationDTO)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation();
            accommodationReservation.AccommodationId = accommodationReservationDTO.AccommodationId;
            accommodationReservation.GuestId = accommodationReservationDTO.GuestId;
            accommodationReservation.StartDate = accommodationReservationDTO.StartDate;
            accommodationReservation.EndDate = accommodationReservationDTO.EndDate;
            accommodationReservation.DurationDays = accommodationReservationDTO.DurationDays;

            accommodationReservationRepository.Save(accommodationReservation);

        }

    }


    

}
