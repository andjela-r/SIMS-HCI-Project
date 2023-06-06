using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.Model;
using InitialProject.Repository;
namespace InitialProject.Service
{
    public class TourRequestService
    {
        TourRequestRepository tourRequestRepository = new TourRequestRepository();

        public void CreateRequest(TourRequest request)
        {
            tourRequestRepository.Save(request);
        }

        public void UpdateStatus(TourRequest request)
        {
            var diff = request.StartDate - DateTime.Now;

            if (diff.Hours <= 48)
            {
                request.Status = RequestStatusEnum.Denied;
            }
        }

        public bool IsTouristNumberLessThanMaximum(TourRequest tourRequest, string touristNumber)
        {
            bool isLess = false;
            if (int.TryParse(touristNumber, out int parsedGuestNumber) && parsedGuestNumber <= tourRequest.MaxTourists)
            {
                isLess = true;
            }

            return isLess;
        }

        public bool IsContainingLanguageWords(TourRequest tourRequest, string[] languageWords)
        {
            bool containsAllWords = true;
            foreach (string word in languageWords)
            {
                if (!tourRequest.Language.ToLower().Contains(word))
                {
                    containsAllWords = false;
                    break;
                }
            }
            return containsAllWords;
        }

        public bool IsWithinDateRange(TourRequest tourRequest, DateTime startDate, DateTime endDate)
        {
            bool isWithinRange = false;

            if (tourRequest.StartDate <= startDate && tourRequest.EndDate >= endDate)
            {
                isWithinRange = true;
            }
            
            return isWithinRange;
        }

        public int GetNumberOfTourRequestsForStatistcs(string language, string locationId, string year)
        {
            List<TourRequest> tourRequests = tourRequestRepository.FindAll();
            int numberOfGuests = 0;
            foreach (TourRequest tourRequest in tourRequests)
            {
                if (string.IsNullOrEmpty(year))
                {
                    if ((string.IsNullOrEmpty(locationId) || int.Parse(locationId) == tourRequest.LocationId) &&
                        (language == tourRequest.Language.ToLower() || string.IsNullOrEmpty(language)))
                    {
                        numberOfGuests++;
                    }
                }
            }
            return numberOfGuests;
        }

        public string RecommendTourLanguage()
        {
            List<TourRequest> tourRequests = tourRequestRepository.FindRequestWithinLastYear();

            string language = tourRequestRepository.FindMostFrequentLanguage(tourRequests);

            return language;
        }

        public int RecommendTourLocation()
        {
            List<TourRequest> tourRequests = tourRequestRepository.FindRequestWithinLastYear();

            int locationId = tourRequestRepository.FindMostFrequentLocationId(tourRequests);

            return locationId;
        }
        
    }
}
