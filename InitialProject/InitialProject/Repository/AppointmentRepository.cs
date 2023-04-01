using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class AppointmentRepository
    {
        private const string FilePath = "../../../Resources/Data/appointments.csv";

        private readonly Serializer<Appointment> _serializer;

        private readonly TourRepository _tourRepository;

        private List<Appointment> _appointments;

        public AppointmentRepository()
        {
            _serializer = new Serializer<Appointment>();
            _appointments = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }

        public Appointment Save(Appointment appointment)
        {
            appointment.Id = NextId();
            appointment.Status = Status.NotStarted;
            appointment.GuestsId = new List<int>();
            _appointments = _serializer.FromCSV(FilePath);
            _appointments.Add(appointment);
            _serializer.ToCSV(FilePath, _appointments);
            return appointment;
        }

        public int NextId()
        {
            _appointments = _serializer.FromCSV(FilePath);
            if (_appointments.Count < 1)
            {
                return 1;
            }

            return _appointments.Max(c => c.Id) + 1;
        }

        public List<Appointment> FindTodaysAppointments()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Today: " + today + "\n");

            _appointments = _serializer.FromCSV(FilePath);

            return _appointments.FindAll(t => t.StartTime.Date == today);
        }

        public Appointment Update(Appointment appointment)
        {
            _appointments = _serializer.FromCSV(FilePath);
            Appointment current = _appointments.Find(c => c.Id == appointment.Id);
            int index = _appointments.IndexOf(current);
            _appointments.Remove(current);
            _appointments.Insert(index, appointment);
            _serializer.ToCSV(FilePath, _appointments);
            return appointment;
        }

        public void StartTodaysAppointment(int id)
        {
            Appointment result = _appointments.Find(x => x.Id == id);
            result.Status = Status.Ongoing;
            Update(result);
            _serializer.ToCSV(FilePath, _appointments);

            TourRepository tourRepository = new TourRepository();
            Tour tour = tourRepository.FindById(id);

        }

        public void TodaysAppointments()
        {
            TourRepository tourRepository = new TourRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            List<Appointment> appointments = appointmentRepository.FindTodaysAppointments();
            foreach (Appointment appointment in appointments)
            {
                Tour tour = tourRepository.FindById(appointment.TourId);
                Console.WriteLine(appointment.Id + " " + tour.Name);
            }
        }
        public Appointment SelectAppointment()
        {
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            TourRepository tourRepository = new TourRepository();

            Console.WriteLine("\nSelect a tour(id)");
            int selectedAppointmentId = Convert.ToInt32(Console.ReadLine());
            Appointment selectedAppointment = appointmentRepository.FindById(selectedAppointmentId);
            appointmentRepository.StartTodaysAppointment(selectedAppointmentId);

            return selectedAppointment;
        }

        public List<Appointment> FindAllTours()
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments;
        }

        public Appointment FindById(int id)
        {
            return _appointments.Find(x => x.Id == id);
        }

        public List<Appointment> FindByLocation(int locationId)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByLocation(locationId);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<Appointment> FindByDuration(float duration)

        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByDuration(duration);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<Appointment> FindByLanguage(string language)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByLanguage(language);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<Appointment> FindByGuestNumber(int numberOfGuests)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByGuestNumber(Convert.ToInt32(numberOfGuests));
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public void FillAppointmentTourDetails(List<Appointment> appointments)
        {
            foreach (var appointment in appointments) appointment.Tour = _tourRepository.FindById(appointment.TourId);
        }
    }
}
