using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.Generic;

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
            tour.Duration = tourDTO.Duration;
            tour.Pictures = tourDTO.Pictures;

            tourRepository.Save(tour);
        }

        public void TrackTourLive()
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            TourRepository tourRepository = new TourRepository();

            appointmentRepository.TodaysAppointments();

            Appointment selectedAppointment = appointmentRepository.SelectAppointment();

            int selectedTourId = selectedAppointment.TourId;
            Tour selectedTour = tourRepository.FindById(selectedTourId);

            List<KeyPoint> keyPoints = keyPointRepository.FindKeyPoints(selectedTour);
            List<int> keyPointsIds = selectedTour.KeyPointsId;

            List<int> touristsToArrive = selectedAppointment.GuestsId;

            tourRepository.InitiateTour(keyPoints, touristsToArrive, keyPointRepository);

            foreach (int id in keyPointsIds)
            {
                KeyPoint keyPoint = keyPointRepository.FindById(id);
                if (keyPoint.Status != Status.Finished)
                {
                    keyPointRepository.SelectKeyPoint(keyPoints, keyPointsIds, touristsToArrive);
                }
            }

            tourRepository.FinishTour(selectedAppointment, keyPoints);
        }
    }
}
