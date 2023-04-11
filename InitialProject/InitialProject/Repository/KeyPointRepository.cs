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
    public class KeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/keyPoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private List<KeyPoint> _keyPoints;

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _keyPoints = _serializer.FromCSV(FilePath);
        }

        public KeyPoint FindById(int id)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints.Find(u => u.Id == id);
        }

        public List<KeyPoint> FindAllByTourAppointment(TourAppointment tourAppointment)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            List<int> keyPointIds = tourAppointment.KeyPointIds;
            List<KeyPoint> retKeyPoints= new List<KeyPoint>();

            foreach(int keyPointId in keyPointIds)
            {
                KeyPoint keyPoint = FindById(keyPointId);
                retKeyPoints.Add(keyPoint);
            }

            return retKeyPoints;
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            KeyPoint current = _keyPoints.Find(c => c.Id == keyPoint.Id);
            int index = _keyPoints.IndexOf(current);
            _keyPoints.Remove(current);
            _keyPoints.Insert(index, keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            keyPoint.Status = Status.NotStarted;
            keyPoint.ArrivedIds = new List<int>();
            _keyPoints = _serializer.FromCSV(FilePath);
            _keyPoints.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;
        }

        public int NextId()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            if (_keyPoints.Count < 1)
            {
                return 1;
            }
            return _keyPoints.Max(c => c.Id) + 1;
        }

    }
}
