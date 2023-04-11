using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RequestStatusRepository
    {
        private const string FilePath = "../../../Resources/Data/requestStatus.csv";
        private readonly Serializer<RequestStatus> _serializer;
        private List<RequestStatus> _requests;

        public RequestStatusRepository()
        {
            _serializer = new Serializer<RequestStatus>();
            _requests = _serializer.FromCSV(FilePath);
        }

        public RequestStatus Save(RequestStatus request)
        {
            request.Id = NextId();
            _requests = _serializer.FromCSV(FilePath);
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }

        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(c => c.Id) + 1;
        }

        public RequestStatus FindById(int id)
        {
            return _requests.Find(x => x.Id == id);
        }

        public List<RequestStatus> FindByReservationId(int reservationId)
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests.FindAll(u => u.ReservationId <= reservationId);
        }
    }
}