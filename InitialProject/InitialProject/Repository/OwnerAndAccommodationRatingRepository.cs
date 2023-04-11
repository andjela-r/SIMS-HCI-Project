using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class OwnerAndAccommodationRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/accRatings.csv";

        private readonly Serializer<OwnerAndAccommodationRating> _serializer;

        private List<OwnerAndAccommodationRating> _ownerAndAccommodationRatings;

        public OwnerAndAccommodationRatingRepository()
        {
            _serializer = new Serializer<OwnerAndAccommodationRating>();
            _ownerAndAccommodationRatings = _serializer.FromCSV(FilePath);
        }

        public OwnerAndAccommodationRating Save(OwnerAndAccommodationRating ownerAndAccommodationRating)
        {
            ownerAndAccommodationRating.Id = NextId();
            _ownerAndAccommodationRatings = _serializer.FromCSV(FilePath);
            _ownerAndAccommodationRatings.Add(ownerAndAccommodationRating);
            _serializer.ToCSV(FilePath, _ownerAndAccommodationRatings);
            return ownerAndAccommodationRating;
        }

        public int NextId()
        {
            _ownerAndAccommodationRatings = _serializer.FromCSV(FilePath);
            if (_ownerAndAccommodationRatings.Count < 1)
            {
                return 1;
            }
            return _ownerAndAccommodationRatings.Max(c => c.Id) + 1;
        }

        public List<OwnerAndAccommodationRating> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<OwnerAndAccommodationRating> FindByOwnerId(int id)
        {
            _ownerAndAccommodationRatings = _serializer.FromCSV(FilePath);
            return _ownerAndAccommodationRatings.FindAll(u => u.OwnerId == id);
        }






    }
}
