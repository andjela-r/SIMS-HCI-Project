using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class TourReservation: ISerializable
    {
        public int Id { get; set; }
        public int NumberOfTourists { get; set; }
        public int GuestId { get; set; }
        public int TourId { get; set; }

        public TourReservation() { }

        public TourReservation(int numberOfTourists, int guestId, int tourId)
        {
            this.NumberOfTourists = numberOfTourists;
            this.GuestId = guestId;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), NumberOfTourists.ToString(), GuestId.ToString(), TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfTourists = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            TourId = Convert.ToInt32(values[3]);
        }
    }
}
