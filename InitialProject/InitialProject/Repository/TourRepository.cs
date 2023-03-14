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

        public Tour FindByLocation(string? city, string? country)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.Location.City == city || u.Location.Country == country);
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

        public Tour FindByGuestNumber(int NumberOfGuests)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(u => u.MaxGuests >= NumberOfGuests);
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
    }
}
