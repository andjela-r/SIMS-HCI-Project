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
        private const string FilePath = "../../../Resources/Data/tour.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public Tour FindByLocation(int locationId)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.LocationId == locationId);
        }

        public Tour FindByDuration(float duration)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.Duration == duration);
        }

        public Tour FindByLanguage(string language)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.Language == language);
        }

        public Tour FindByGuestNumber(int numberOfGuests)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.MaxGuests >= numberOfGuests);
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
