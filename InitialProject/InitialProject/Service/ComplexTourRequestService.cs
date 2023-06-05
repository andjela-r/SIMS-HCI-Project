using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class ComplexTourRequestService
    {
       ComplexTourRequestRepository _complexTourRequestRepository = new ComplexTourRequestRepository();
       PartOfComplexTourRequestRepository _partOfComplexTourRequestRepository = new PartOfComplexTourRequestRepository();

       public void CreatePartOfComplexTourRequest(TourRequest tourRequest)
       {
           _partOfComplexTourRequestRepository.Save(tourRequest);

       }

       public void CreateComplexTourRequest(ComplexTourRequest complexTourRequest)
       {
           _complexTourRequestRepository.Save(complexTourRequest);
       }

       public void AddPartOfComplexTourRequestToRequest(ComplexTourRequest complexTourRequest, TourRequest tourRequest)
       {
            var idToAdd = tourRequest.Id;
            complexTourRequest.PartIds.Add(idToAdd);
            _complexTourRequestRepository.Save(complexTourRequest);

       }

       public void UpdateTotalStatus(ComplexTourRequest complexTourRequest)
       {
           foreach (var partId in complexTourRequest.PartIds)
           {
               var part = _partOfComplexTourRequestRepository.FindById(partId);
               if (part != null)
               {
                    //Check 48h criteria
                    var today = DateTime.Today;
                    var startDate = part.StartDate;
                    var difference = today - startDate;

                    if (difference.Hours < 48)
                    {
                        part.Status = RequestStatusEnum.Denied;
                        _partOfComplexTourRequestRepository.Save(part);
                    }
               }
           }
           //Check all accepted criteria
            var acceptedPartsNumber = _partOfComplexTourRequestRepository.FindAll()
               .Where(x => x.Status == RequestStatusEnum.Accepted).Count();
            var partsNumber = _partOfComplexTourRequestRepository.FindAll().Count();
            if (acceptedPartsNumber == partsNumber)
            {
                complexTourRequest.TotalStatus = RequestStatusEnum.Accepted;
                _complexTourRequestRepository.Save(complexTourRequest);
            }
       }
    }
}
