using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
