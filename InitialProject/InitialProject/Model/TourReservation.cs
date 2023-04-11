using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class TourReservation: ISerializable
    {
        public int Id { get; set; }
        public int NumberOfTourists { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }

        public TourReservation() { }

        public TourReservation(int numberOfTourists, int touristId, int tourId)
        {
            this.NumberOfTourists = numberOfTourists;
            this.TouristId = touristId;
            this.TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), NumberOfTourists.ToString(), TouristId.ToString(), TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfTourists = Convert.ToInt32(values[1]);
            TouristId = Convert.ToInt32(values[2]);
            TourId = Convert.ToInt32(values[3]);
        }
    }
}
