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
            accommodation.MaxOccupancy = accommodationDTO.MaxOccupancy;
            accommodation.MinDays = accommodationDTO.MinDays;
            accommodation.CancelPeriod = accommodationDTO.CancelPeriod;
            accommodation.PicturesId = accommodationDTO.PicturesId;

            accommodationRepository.Save(accommodation);
        }

       











    }
}
