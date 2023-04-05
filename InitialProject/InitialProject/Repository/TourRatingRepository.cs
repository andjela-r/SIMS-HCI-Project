using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRatings.csv";

        private readonly Serializer<TourRating> _serializer;

        private List<TourRating> _tourRatings;

        public TourRatingRepository()
        {
            _serializer = new Serializer<TourRating>();
            _tourRatings = _serializer.FromCSV(FilePath);
        }

        public TourRating Save(TourRating tourRating)
        {
            tourRating.Id = NextId();
            _tourRatings = _serializer.FromCSV(FilePath);
            _tourRatings.Add(tourRating);
            _serializer.ToCSV(FilePath, _tourRatings);
            return tourRating;
        }

        public int NextId()
        {
            _tourRatings = _serializer.FromCSV(FilePath);
            if (_tourRatings.Count < 1) return 1;
            return _tourRatings.Max(c => c.Id) + 1;
        }
    }
}
