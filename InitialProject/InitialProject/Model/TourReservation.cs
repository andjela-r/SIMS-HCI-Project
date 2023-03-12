using System;

namespace InitialProject.Model
{


    public class TourReservation
    {
        public int GuestId { get; set; }
        public int TourId { get; set; }

        public TourReservation() { }

        public TourReservation(int guestId, int tourId)
        {
            this.GuestId = guestId;
            this.TourId = tourId;
        }
    }
}
