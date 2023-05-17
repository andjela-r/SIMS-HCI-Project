using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Service
{
    public class TourRequestStatisticsService
    {
        public TourRequestService tourRequestService = new TourRequestService();
        TourRequestRepository tourRequestRepository = new TourRequestRepository();

        public double GetStatisticsByStatus(int touristId, int status, DateTime? year = null)
        {
            var allRequests = tourRequestRepository.FindByTouristId(touristId);

            if (year is not null)
            {
                allRequests = allRequests.Where(x => x.StartDate.Year == year.Value.Year).ToList();
            }

            var numTotalNonWaitingRequest = allRequests.Count(x => x.Status != RequestStatusEnum.Waiting);
            var statusRequestCount = allRequests.Count(x => x.Status == (RequestStatusEnum)status);


            return ((double)statusRequestCount / (double)numTotalNonWaitingRequest) * 100.0f;
        }

        public int GetRequestCountByLanguage(int touristId, string language)
        {
            var allRequests = tourRequestRepository.FindByTouristId(touristId);

            var result = allRequests.Count(x => x.Language == language); 

            return result;
        }

        public int GetRequestCountByLocation(int touristId, int locationId)
        {
            var allRequests = tourRequestRepository.FindByTouristId(touristId);
            var result = allRequests.Count(x => x.LocationId == locationId);
            return result;
        }

        public double GetAverageNumberOfTouristsInAcceptedTourRequestsByYear(int touristId, DateTime? year = null)
        {
            var allRequests = tourRequestRepository.FindByTouristId(touristId);

            if (year is not null)
            {
                allRequests = allRequests.Where(x => x.StartDate.Year == year.Value.Year).ToList();
            }

            var allAcceptedRequests = allRequests.Where(x => x.Status == RequestStatusEnum.Accepted).ToList();

            var touristSum = allAcceptedRequests.Sum(x => x.MaxTourists);
            var average = touristSum / allAcceptedRequests.Count();

            return average;
        } 
    }
}
