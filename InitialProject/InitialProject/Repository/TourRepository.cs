using InitialProject.DTO;
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

        //TODO FindAllTours()
        public List<Tour> FindAllTours()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public List<Tour> FindByLocation(int locationId)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.LocationId == locationId);
        }

        public List<Tour> FindByDuration(float duration)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.Duration <= duration);
        }

        public List<Tour> FindByLanguage(string language)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.Language == language);
        }

        public List<Tour> FindByGuestNumber(int numberOfGuests)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(u => u.MaxGuests >= numberOfGuests);
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
            _tours= _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(c => c.Id) + 1;
        }

        public List<Tour> FindTodaysTours()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Danas: " +today);

            _tours = _serializer.FromCSV(FilePath);

            return _tours.FindAll(t => t.StartTime == today);
        }
    }
}
