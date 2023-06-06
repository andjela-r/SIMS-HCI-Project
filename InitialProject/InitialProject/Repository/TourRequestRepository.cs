using System;
using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.View.Tourist;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

        public List<TourRequest> FindByLanguage(string language)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FindAll(x => x.Language == language);
        }

        public List<TourRequest> FindByLocationId(Location location)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FindAll(x => x.Location == location);
        }

        public List<TourRequest> FindRequestWithinLastYear()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FindAll(x => x.StartDate >= DateTime.Today.AddYears(-1));
        }
        public string FindMostFrequentLanguage(List<TourRequest> tourRequests)
        {
            Dictionary<string, int> valueCounts = new Dictionary<string, int>();

            string[] lines = System.IO.File.ReadAllLines(FilePath);
            
            foreach (TourRequest tourRequest in tourRequests)
            {
                List<string> values = new List<string>();
                values.Add(tourRequest.Language);

                if (valueCounts.ContainsKey(tourRequest.Language))
                {
                    valueCounts[tourRequest.Language]++;
                }
                else
                {
                    valueCounts[tourRequest.Language] = 1;
                }
            }

            string mostFrequentLanguage = valueCounts.OrderByDescending(kv => kv.Value).FirstOrDefault().Key;
            return mostFrequentLanguage;
        }

        public int FindMostFrequentLocationId(List<TourRequest> tourRequests)
        {
            Dictionary<int, int> valueCounts = new Dictionary<int, int>();

            string[] lines = System.IO.File.ReadAllLines(FilePath);

            foreach (TourRequest tourRequest in tourRequests)
            {
                List<string> values = new List<string>();
                values.Add(tourRequest.Language);

                if (valueCounts.ContainsKey(tourRequest.LocationId))
                {
                    valueCounts[tourRequest.LocationId]++;
                }
                else
                {
                    valueCounts[tourRequest.LocationId] = 1;
                }
            }

            int mostFrequentLocation = valueCounts.OrderByDescending(kv => kv.Value).FirstOrDefault().Key;
            return mostFrequentLocation;
        }

        public List<int> FindAllYears()
        {
            List<TourRequest> allTourRequests = FindAll();
            List<int> years = new List<int>();

            foreach (TourRequest tourRequest in allTourRequests)
            { 
                years.Add(tourRequest.StartDate.Year);
                years.Add(tourRequest.EndDate.Year);
            }

            List<int> distinctYears = years.Distinct().ToList();

            return distinctYears;
        }
    }
}
