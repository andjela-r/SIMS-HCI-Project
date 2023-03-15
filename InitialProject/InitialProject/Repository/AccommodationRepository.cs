using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Type = InitialProject.Model.Type;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }

        public Accommodation FindByName(string name)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(u => u.Name == name);
        }

        public Accommodation FindByType(Type type)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(u => u.Type == type);
        }

        public Accommodation FindByOccupancy(int maxOccupancy)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(u => u.MaxOccupancy <= maxOccupancy);
        }

        public Accommodation FindByMinDays(int minDays)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(u => u.MinDays >= minDays);
        }

        public Accommodation FindByLocation(string? city, string? country)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FirstOrDefault(u => u.Location.City == city || u.Location.Country == country);
        }

    }
}
