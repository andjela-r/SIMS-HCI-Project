using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class TourService
    {
        TourRepository tourRepository = new TourRepository();
        TourReservationRepository tourReservationRepository = new TourReservationRepository();
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
