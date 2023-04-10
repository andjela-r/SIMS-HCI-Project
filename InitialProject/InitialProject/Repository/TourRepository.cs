using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{

    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> FindAll()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public Tour FindById(int id)
        {
            return _tours.Find(x => x.Id == id);
        }

        public List<Tour> FindByLocation(int locationId)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.LocationId == locationId);
        }

        public List<Tour> FindByDuration(float duration)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.Duration == duration);
        }

        public List<Tour> FindByLanguage(string language)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.Language == language);
        }

        public List<Tour> FindByGuestNumber(int numberOfTourists)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.MaxTourists == numberOfTourists);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1) return 1;
            return _tours.Max(c => c.Id) + 1;
        }

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(c => c.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }
    }
}
