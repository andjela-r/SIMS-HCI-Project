using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    internal class AppointmentRepository
    {
        private const string FilePath = "../../../Resources/Data/appointments.csv";

        private readonly Serializer<Appointment> _serializer;

        private List<Appointment> _appointments;

        public AppointmentRepository()
        {
            _serializer = new Serializer<Appointment>();
            _appointments = _serializer.FromCSV(FilePath);
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
            Console.WriteLine("Today: " +today+"\n");

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

        public Appointment FindById(int id)
        {
            return _appointments.Find(x => x.Id == id);
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
    }
}
