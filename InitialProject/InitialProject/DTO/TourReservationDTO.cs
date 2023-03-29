using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourReservationDTO
    {
        public int TourId { get; set; }
        public int GuestId { get; set; }
        public int NumberOfTourists { get; set; }

        public TourReservationDTO() { }

        public TourReservationDTO(int tourId, int guestId, int numberOfTourists)
        {
            this.TourId = tourId;
            this.GuestId = guestId;
            this.NumberOfTourists = numberOfTourists;
        }
    }
}
