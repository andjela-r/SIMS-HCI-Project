using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class AppointmentDTO
    {
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public int GuideId { get; set; }
        public List<int> GuestsId { get; set; }

        public AppointmentDTO() { }

        public AppointmentDTO(int tourId, DateTime startTime, int guideId, List<int> guestsId)
        {
            TourId = tourId;
            StartTime = startTime;
            GuideId = guideId;
            GuestsId = guestsId;
        }
    }
}
