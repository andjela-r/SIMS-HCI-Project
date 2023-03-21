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
            //appointment.GuestsId = new List<int>();
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
            Console.WriteLine("Danas: " +today+"\n");

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
            Appointment result = _appointments.Find(x => x.TourId == id);
            //Console.WriteLine("Izabrana tura: "+result.TourId);
            result.Status = Status.Ongoing;
            Update(result);
            _serializer.ToCSV(FilePath, _appointments);

            TourRepository tourRepository = new TourRepository();
            Tour tour = tourRepository.FindById(id);

            //return tour;
        }
        //Anjdeline
        public List<Appointment> FindAllTours()
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments;
        }

        public Appointment FindById(int id)
        {
            return _appointments.Find(x => x.Id == id);
        }

        /*public List<Appointment> FindByLocation(int locationId)
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments.FindAll(u => u.LocationId == locationId);
        }

        public List<Appointment> FindByDuration(float duration)
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments.FindAll(u => u.Duration <= duration);
        }

        public List<Appointment> FindByLanguage(string language)
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments.FindAll(u => u.Language == language);
        }

        public List<Appointment> FindByGuestNumber(int numberOfGuests)
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments.FindAll(u => u.MaxGuests >= numberOfGuests);
        }*/
    }
}
