using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class PartOfComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/partOfComplexTour.csv";
        private readonly Serializer<TourRequest> _serializer;
        private List<TourRequest> _partOfComplexTourRequests;

        public PartOfComplexTourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
            if (_partOfComplexTourRequests.Count < 1)
            {
                return 1;
            }
            return _partOfComplexTourRequests.Max(c => c.Id) + 1;
        }

        public List<TourRequest> FindAll()
        {
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
            return _partOfComplexTourRequests;
        }

        public TourRequest FindById(int id)
        {
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
            return _partOfComplexTourRequests.FirstOrDefault(x => x.Id == id);
        }

        public List<TourRequest> FindByTouristId(int touristId)
        {
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
            return _partOfComplexTourRequests.FindAll(x => x.TouristId == touristId);
        }
        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _partOfComplexTourRequests = _serializer.FromCSV(FilePath);
            _partOfComplexTourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _partOfComplexTourRequests);
            return tourRequest;
        }
    }
}
