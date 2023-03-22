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
    public class AppointmentService
    {
        AppointmentRepository appointmentRepository = new AppointmentRepository();

        public void CreateAppointment(AppointmentDTO appointmentDTO)
        {
            Appointment appointment = new Appointment();

            appointment.TourId = appointmentDTO.TourId;
            appointment.StartTime = appointmentDTO.StartTime;
            appointment.GuideId = appointmentDTO.GuideId;

            appointmentRepository.Save(appointment);
        }
    }
}
