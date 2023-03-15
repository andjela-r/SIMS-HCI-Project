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
    public class TourService
    {
        TourRepository tourRepository = new TourRepository();
        public void CreateTour(TourDTO tourDTO)
        {
            Tour tour = new Tour();

            tour.Name = tourDTO.Name;
            tour.LocationId = tourDTO.LocationId;
            tour.Description = tourDTO.Description;
            tour.Language = tourDTO.Language;
            tour.MaxGuests = tourDTO.MaxGuests;
            tour.KeyPointsId = tourDTO.KeyPointsId;
            tour.StartTime = tourDTO.StartTime;
            tour.Duration = tourDTO.Duration;
            tour.PicturesId = tourDTO.PicturesId;

            tourRepository.Save(tour);
        }
    }
}
