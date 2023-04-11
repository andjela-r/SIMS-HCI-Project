using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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
            tour.MaxTourists = tourDTO.MaxTourists;
            tour.KeyPointsId = tourDTO.KeyPointsId;
            tour.Duration = tourDTO.Duration;
            tour.Pictures = tourDTO.Pictures;
            tour.GuideId = tourDTO.GuideId;

            tourRepository.Save(tour);
        }

        public TourAppointment SelectTour()
        {
            TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            DateTime today = tourAppointmentRepository.FindTodaysDate();
            Console.WriteLine("Today: " + today + "\n");

            List<TourAppointment> appointments = tourAppointmentRepository.FindTodaysAppointments(today);
            foreach (TourAppointment appointment in appointments)
            {
                Tour tour = tourRepository.FindById(appointment.TourId);
                Console.WriteLine(appointment.Id + " " + tour.Name);
            }

            Console.WriteLine("\nSelect a tour(id)");
            TourAppointment selectedAppointment = tourAppointmentService.SelectAppointment();

            return selectedAppointment;
        }

        public void UpdateKeyPoints(TourAppointment selectedAppointment)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            TourService tourService = new TourService();
            KeyPointService keyPointService = new KeyPointService();

            List<KeyPoint> keyPoints = keyPointRepository.FindAllByTourAppointment(selectedAppointment);
            List<int> keyPointsIds = selectedAppointment.KeyPointIds;

            List<int> touristsToArrive = selectedAppointment.TouristIds;

            tourService.InitiateTour(keyPoints, touristsToArrive);

            keyPointService.SelectKeyPoint(keyPoints, keyPointsIds, touristsToArrive);
        }

        public void TrackTourLive()
        {
            KeyPointService keyPointService = new KeyPointService();
            TourRepository tourRepository = new TourRepository();
            TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();

            TourAppointment selectedAppointment = SelectTour();
            
            UpdateKeyPoints(selectedAppointment);

            Console.WriteLine("\n-----------------\nTour is finished.\n-----------------\n");
        }

        public void InitiateTour(List<KeyPoint> keyPoints, List<int> touristsToArrive)
        {
            KeyPointService keyPointService = new KeyPointService();

            KeyPoint firstKeypoint = keyPoints.First();
            keyPointService.InitiateKeyPoint(firstKeypoint, keyPoints, touristsToArrive);
        }
    }
}
