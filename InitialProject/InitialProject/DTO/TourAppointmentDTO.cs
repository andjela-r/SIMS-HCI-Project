using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourAppointmentDTO
    {
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }

        public TourAppointmentDTO() { }

        public List<int> KeyPointIds { get; set; }

        public TourAppointmentDTO(int tourId, DateTime startTime, List<int> keyPointIds)
        {
            TourId = tourId;
            StartTime = startTime;
            KeyPointIds = keyPointIds;
        }
    }
}
