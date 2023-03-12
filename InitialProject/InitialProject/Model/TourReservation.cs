using System;

namespace InitialProject.Model
{


    public class TourReservation
    {
        public int Id { get; set; }
        public int NumberOfGuests { get; set; }
        public int GuestId { get; set; }
        public int TourId { get; set; }

        public TourReservation() { }

        public TourReservation(int id, int numberOfGuests, int guestId, int tourId)
        {
            this.Id = id;
            this.NumberOfGuests = numberOfGuests;
            this.GuestId = guestId;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), NumberOfGuests.ToString(), GuestId.ToString(), TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfGuests = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            TourId = Convert.ToInt32(values[3]);
        }
    }
}
