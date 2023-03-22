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

        public List<KeyPoint> FindKeyPoints(Tour tour)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            List<int> keyPointsId = tour.KeyPointsId;
            List<KeyPoint> retKeyPoints= new List<KeyPoint>();

            foreach(int keyPointId in keyPointsId)
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

        public List<int> InitiateKeyPoint(KeyPoint keyPoint, List<KeyPoint> keyPoints, List<int> touristsToArrive)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            keyPoint.Status = Status.Ongoing;
            keyPointRepository.Update(keyPoint);

            List<int> arrivedTourists = new List<int>();

            Console.WriteLine("\nTourists to arrive: ");
            touristsToArrive.ForEach(Console.WriteLine);

            foreach (int tourist in touristsToArrive)
            {
                Console.WriteLine(tourist + "Arrived: (y/n)");
                string arrived = Console.ReadLine();

                if (arrived == "y")
                {
                    arrivedTourists.Add(tourist);
                }
            }

            keyPoint.ArrivedIds = arrivedTourists;

            arrivedTourists.ForEach(Console.WriteLine);

            FinishKeyPoint(keyPoint, keyPoints);

            return arrivedTourists;
        }

        public static void FinishKeyPoint(KeyPoint keyPoint, List<KeyPoint> keyPoints)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            Console.WriteLine("\nStarted keyPoint " + keyPoint.Name + ". Do you want to finish key point? (y) ");
            string response = Console.ReadLine();
            switch (response)
            {
                case "y":
                    keyPoint.Status = Status.Finished;
                    keyPointRepository.Update(keyPoint);
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }

        }

        public void SelectKeyPoint(List<KeyPoint> keyPoints, List<int> keyPointsId, List<int> touristsToArrive)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();

            Console.WriteLine("\nAvailable key points:");
            foreach (int id in keyPointsId)
            {
                KeyPoint keyPoint = keyPointRepository.FindById(id);
                if (keyPoint.Status != Status.Finished)
                    Console.WriteLine(keyPoint.Id + " " + keyPoint.Name);
            }

            Console.WriteLine("\nSelect key point: ");
            int keyPointId = Convert.ToInt32(Console.ReadLine());

            KeyPoint selectedKeyPoint = keyPointRepository.FindById(keyPointId);
            InitiateKeyPoint(selectedKeyPoint, keyPoints, touristsToArrive);
        }

    }
}
