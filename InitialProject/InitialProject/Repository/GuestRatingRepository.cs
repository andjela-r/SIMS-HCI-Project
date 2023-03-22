using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class GuestRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/guestsRatings.csv";

        private readonly Serializer<GuestRating> _serializer;

        private List<GuestRating> _guestRatings;

        public GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _guestRatings = _serializer.FromCSV(FilePath);
        }

        public GuestRating Save(GuestRating guestRating)
        {
            guestRating.Id = NextId();
            _guestRatings = _serializer.FromCSV(FilePath);
            _guestRatings.Add(guestRating);
            _serializer.ToCSV(FilePath, _guestRatings);
            return guestRating;
        }

        public int NextId()
        {
            _guestRatings = _serializer.FromCSV(FilePath);
            if (_guestRatings.Count < 1)
            {
                return 1;
            }
            return _guestRatings.Max(c => c.Id) + 1;
        }

    }
}
