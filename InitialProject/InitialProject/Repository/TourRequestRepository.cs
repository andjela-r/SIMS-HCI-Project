using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.View.Tourist;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class TourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";
        private readonly Serializer<TourRequest> _serializer;
        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(c => c.Id) + 1;
        }

        public List<TourRequest> FindAll()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests;
        }

        public List<TourRequest> FindByTouristId(int touristId)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FindAll(x => x.TouristId == touristId);
        }
        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

    }
}
