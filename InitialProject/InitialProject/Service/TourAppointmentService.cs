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
    public class TourAppointmentService
    {
        TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();

        public void CreateAppointment(TourAppointmentDTO tourAppointmentDTO)
        {
            TourAppointment tourAppointment = new TourAppointment();

            tourAppointment.TourId = tourAppointmentDTO.TourId;
            tourAppointment.StartTime = tourAppointmentDTO.StartTime;
            tourAppointment.KeyPointIds = tourAppointmentDTO.KeyPointIds;

            tourAppointmentRepository.Save(tourAppointment);
        }

        public TourAppointment SelectAppointment()
        {
            TourAppointmentRepository appointmentRepository = new TourAppointmentRepository();
            TourRepository tourRepository = new TourRepository();

            int selectedAppointmentId = Convert.ToInt32(Console.ReadLine());
            TourAppointment selectedAppointment = appointmentRepository.FindById(selectedAppointmentId);
            appointmentRepository.StartTodaysAppointment(selectedAppointmentId);

            return selectedAppointment;
        }
    }
}
