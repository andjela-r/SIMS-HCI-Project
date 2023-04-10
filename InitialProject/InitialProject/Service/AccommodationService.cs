using InitialProject.DTO;
using InitialProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class AccommodationService
    {
        AccommodationRepository accommodationRepository = new AccommodationRepository();
        public void CreateAccommodation(AccommodationDTO accommodationDTO)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Name = accommodationDTO.Name;
            accommodation.LocationId = accommodationDTO.LocationId; 
            accommodation.Type = accommodationDTO.Type;
            accommodation.MaxGuests = accommodationDTO.MaxGuests;
            accommodation.MinStay = accommodationDTO.MinStay;
            accommodation.DaysToCancelBeforeReservation = accommodationDTO.DaysToCancelBeforeReservation;
            accommodation.Pictures = accommodationDTO.Pictures;

            accommodationRepository.Save(accommodation);
        }

       











    }
}
