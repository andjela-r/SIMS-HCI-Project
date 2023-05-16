using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class RenovationService
    {
        RenovationRepository renovationRepository = new RenovationRepository();
        public void CreateRenovation(Renovation renovation)
        {
            Renovation newRenovation = new Renovation();
            newRenovation.StartDate = renovation.StartDate;
            newRenovation.EndDate = renovation.EndDate;
            newRenovation.ExpectedDuration=renovation.ExpectedDuration;
            newRenovation.Description=renovation.Description;
            newRenovation.AccommodationId = renovation.AccommodationId; 

            renovationRepository.Save(newRenovation);
        }












    }
}
