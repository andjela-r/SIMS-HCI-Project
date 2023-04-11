using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.View.Guide;

namespace InitialProject.Service
{
    public class TourAppointmentService
    {
        TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();
        private TourRepository _tourRepository = new TourRepository();

        public void CreateAppointment(TourAppointmentDTO tourAppointmentDTO)
        {
            TourAppointment tourAppointment = new TourAppointment();

            tourAppointment.TourId = tourAppointmentDTO.TourId;
            tourAppointment.StartTime = tourAppointmentDTO.StartTime;
            tourAppointment.KeyPointIds = tourAppointmentDTO.KeyPointIds;
            var tour = _tourRepository.FindById(tourAppointment.TourId);
            tourAppointment.AvailableSeats = tour.MaxTourists;

            tourAppointmentRepository.Save(tourAppointment);
        }

        public TourAppointment SelectAppointment()
        {
            TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();

            Console.WriteLine("Select an appointment: ");
            int selectedAppointmentId = Convert.ToInt32(Console.ReadLine());
            TourAppointment selectedAppointment = tourAppointmentRepository.FindById(selectedAppointmentId);
            tourAppointmentRepository.StartTodaysAppointment(selectedAppointmentId);

            return selectedAppointment;
        }

        public void CancelUpcomingAppointment()
        {
            TourRepository tourRepository = new TourRepository();
            TourService tourService = new TourService();
            VoucherService voucherService = new VoucherService();
            TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();
            DateTime today = tourAppointmentRepository.FindTodaysDate();
            List<TourAppointment> upcomingAppointments = tourAppointmentRepository.FindUpcomingAppointments(today);

            foreach (TourAppointment appointment in upcomingAppointments)
            {
                Tour tour = tourRepository.FindById(appointment.TourId);
                Console.WriteLine(appointment.Id + "\t" + tour.Name + "\t" + appointment.StartTime);
            }

            TourAppointment selectedAppointment = SelectAppointment();
            int selectedTourId = selectedAppointment.TourId;
            Tour selectedTour = tourRepository.FindById(selectedTourId);

            DateTime expirationDate = today.AddYears(1);

            foreach (int touristId in selectedAppointment.TouristIds)
            {
                VoucherDTO voucherDTO = new VoucherDTO(selectedTour.Name, touristId, expirationDate);
                voucherService.CreateVoucher(voucherDTO);
            }

            tourAppointmentRepository.Delete(selectedAppointment);
        }
    }
}
