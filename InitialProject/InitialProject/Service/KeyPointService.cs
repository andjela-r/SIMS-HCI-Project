using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Service
{
    public class KeyPointService
    {
        KeyPointRepository keyPointRepository = new KeyPointRepository();

        public void CreateKeyPoint(KeyPointDTO keyPointDTO)
        {
            KeyPoint keyPoint = new KeyPoint();
            keyPoint.Name = keyPointDTO.Name;

            keyPointRepository.Save(keyPoint);
        }

    }
}

