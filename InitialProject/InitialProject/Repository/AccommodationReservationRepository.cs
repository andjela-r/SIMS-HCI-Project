using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservations.Find(u => u.Id == accommodationReservation.Id);
            int index = _accommodationReservations.IndexOf(current);
            _accommodationReservations.Remove(current);
            _accommodationReservations.Insert(index, accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public AccommodationReservation DeleteById(int id)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation reservationToDelete = _accommodationReservations.Find(u => u.Id == id);
            _accommodationReservations.Remove(reservationToDelete);
            _serializer.ToCSV(FilePath, _accommodationReservations);
             return reservationToDelete;     
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

        public List<AccommodationReservation> FindByAccommodationId(int accommodationId)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(u => u.AccommodationId <= accommodationId);
        }

        public List<AccommodationReservation> FindByGuestId(int guestId)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(u => u.GuestId <= guestId);
        }

        public AccommodationReservation FindById(int id)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.Find(u => u.Id == id);
        }

        public List<AccommodationReservation> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

    }

}
