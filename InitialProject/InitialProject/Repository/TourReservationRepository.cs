using System;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";
        private readonly Serializer<TourReservation> _serializer;
        private List<TourReservation> _tourReservations;

        public TourReservationRepository() 
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(c => c.Id) + 1;
        }

        public TourReservation CreateReservation(TourReservation tourReservation) 
        {
            tourReservation.Id = NextId();
            _tourReservations = _serializer.FromCSV(FilePath);
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }
    }
}
