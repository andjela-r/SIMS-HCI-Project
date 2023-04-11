using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class TourAppointmentRepository
    {
        private const string FilePath = "../../../Resources/Data/tourAppointments.csv";

        private readonly Serializer<TourAppointment> _serializer;

        private readonly TourRepository _tourRepository;

        private List<TourAppointment> _appointments;

        public TourAppointmentRepository()
        {
            _serializer = new Serializer<TourAppointment>();
            _appointments = _serializer.FromCSV(FilePath);
            _tourRepository = new TourRepository();
        }

        public TourAppointment Save(TourAppointment appointment)
        {
            appointment.Id = NextId();
            appointment.Status = Status.NotStarted;
            appointment.TouristIds = new List<int>();
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
        
        public TourAppointment Update(TourAppointment appointment)
        {
            _appointments = _serializer.FromCSV(FilePath);
            TourAppointment current = _appointments.Find(c => c.Id == appointment.Id);
            int index = _appointments.IndexOf(current);
            _appointments.Remove(current);
            _appointments.Insert(index, appointment);
            _serializer.ToCSV(FilePath, _appointments);
            return appointment;
        }

        public void StartTodaysAppointment(int id)
        {
            TourAppointment result = _appointments.Find(x => x.Id == id);
            result.Status = Status.Ongoing;
            Update(result);
            //_serializer.ToCSV(FilePath, _appointments);
        }

        public DateTime FindTodaysDate()
        {
            DateTime today = DateTime.Today;
            return today;
        }
        public List<TourAppointment> FindTodaysAppointments(DateTime today)
        {
            _appointments = _serializer.FromCSV(FilePath);

            List<TourAppointment> appointments = _appointments.FindAll(t => t.StartTime.Date == today && t.Status == Status.NotStarted);

            return appointments;
        }

        public List<TourAppointment> FindUpcomingAppointments(DateTime today)
        {
            List<TourAppointment> upcomingAppointments = _appointments.FindAll(t => t.StartTime.Date >= today.AddDays(2));

            return upcomingAppointments;
        }

        public List<TourAppointment> FindAll()
        {
            _appointments = _serializer.FromCSV(FilePath);
            return _appointments;
        }

        public TourAppointment FindById(int id)
        {
            return _appointments.Find(x => x.Id == id);
        }

        public List<TourAppointment> FindByLocation(int locationId)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByLocation(locationId);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<TourAppointment> FindByDuration(float duration)

        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByDuration(duration);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<TourAppointment> FindByLanguage(string language)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByLanguage(language);
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public List<TourAppointment> FindByGuestNumber(int numberOfTourists)
        {
            _appointments = _serializer.FromCSV(FilePath);
            var tours = _tourRepository.FindByGuestNumber(Convert.ToInt32(numberOfTourists));
            var appointments = _appointments.FindAll(a => tours.Any(t => t.Id == a.TourId));

            FillAppointmentTourDetails(appointments);
            return appointments;
        }

        public void FillAppointmentTourDetails(List<TourAppointment> appointments)
        {
            foreach (var appointment in appointments) appointment.Tour = _tourRepository.FindById(appointment.TourId);
        }

        public void Delete(TourAppointment tourAppointment)
        {
            _appointments = _serializer.FromCSV(FilePath);
            TourAppointment found = _appointments.Find(c => c.Id == tourAppointment.Id);
            _appointments.Remove(found);
            _serializer.ToCSV(FilePath, _appointments);
        }
    }
}
