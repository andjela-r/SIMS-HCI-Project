using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InitialProject.Repository
{
    internal class AccommodationReservationRepository
    {

        private const string FilePath = "../../../Resources/Data/accommodationReservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            if (_accommodationReservations.Count < 1)
            {
                return 1;
            }
            return _accommodationReservations.Max(c => c.Id) + 1;
        }

        public AccommodationReservation CreateReservation(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public List<AccommodationReservation> FindByStartDate(DateTime startDate)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(u => u.StartDate <= startDate);
        }

        public List<AccommodationReservation> FindByEndDate(DateTime endDate)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(u => u.EndDate <= endDate);
        }

        public bool IsAvailable(AccommodationReservation newReservation, AccommodationReservation oldReservations)
        {
            if (newReservation.StartDate == oldReservations.StartDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;

        }

        public List<DateTime> GetOccupiedDays(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = GetDatesBetween(startDate, endDate).ToList();
            return dates;
        }

        public List<AccommodationReservation> FindByAccommodationId(int accommodationId)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(u => u.AccommodationId <= accommodationId);
        }

        public AccommodationReservation FindById(int id)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.Find(x => x.Id == id);
        }

    }

}
