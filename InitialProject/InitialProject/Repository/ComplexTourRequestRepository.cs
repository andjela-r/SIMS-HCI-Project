using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";
        private readonly Serializer<ComplexTourRequest> _serializer;
        private List<ComplexTourRequest> _complexTourRequests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            if (_complexTourRequests.Count < 1)
            {
                return 1;
            }
            return _complexTourRequests.Max(c => c.Id) + 1;
        }

        public List<ComplexTourRequest> FindAll()
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            return _complexTourRequests;
        }

        public List<ComplexTourRequest> FindByTouristId(int touristId)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            return _complexTourRequests.FindAll(x => x.TouristId == touristId);
        }
        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = NextId();
            _complexTourRequests = _serializer.FromCSV(FilePath);
            _complexTourRequests.Add(complexTourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
            return complexTourRequest;
        }
    }
}
