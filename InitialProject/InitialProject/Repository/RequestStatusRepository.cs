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
    public class RequestStatusRepository
    {
        private const string FilePath = "../../../Resources/Data/requestsStatuses.csv";
        private readonly Serializer<RequestStatus> _serializer;
        private List<RequestStatus> _requests;

        private List<RequestStatus> _requestStatuses;

        public RequestStatusRepository()
        {
            _serializer = new Serializer<RequestStatus>();
            _requestStatuses = _serializer.FromCSV(FilePath);
        }

        public RequestStatus Save(RequestStatus requestStatus)
        {
            requestStatus.Id = NextId();
            _requestStatuses = _serializer.FromCSV(FilePath);
            _requestStatuses.Add(requestStatus);
            _serializer.ToCSV(FilePath, _requestStatuses);
            return requestStatus;
        }

        public int NextId()
        {
            _requestStatuses = _serializer.FromCSV(FilePath);
            if (_requestStatuses.Count < 1)
            {
                return 1;
            }
            return _requestStatuses.Max(c => c.Id) + 1;
        }

        public List<RequestStatus> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public RequestStatus FindById(int id)
        {
            return _requestStatuses.Find(x => x.Id == id);
        }
        public RequestStatus Update(RequestStatus requestStatus)
        {
            _requestStatuses = _serializer.FromCSV(FilePath);
            RequestStatus current = _requestStatuses.Find(c => c.Id == requestStatus.Id);
            int index = _requestStatuses.IndexOf(current);
            _requestStatuses.Remove(current);
            _requestStatuses.Insert(index, requestStatus);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _requestStatuses);
            return requestStatus;
        }
    }
}