using InitialProject.Model;
using InitialProject.Repository;
using System.Linq;

namespace InitialProject.Service
{
    class TourReservationService
    {
        public static Appointment Book(TourReservation newReservation)
        {
            var appointmentRepository = new AppointmentRepository();
            var tourReservationRepository = new TourReservationRepository();
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var createdReservation = tourReservationRepository.CreateReservation(newReservation);

            for (var i = 0; i < createdReservation.NumberOfTourists; i++)
            {
                var highestId = appointment.TouristsId.Any() ? appointment.TouristsId.Max() : 1;
                appointment.TouristsId.Add(highestId + 1);
                appointment = appointmentRepository.Update(appointment);
            }

            return appointment;
        }
    }
}
