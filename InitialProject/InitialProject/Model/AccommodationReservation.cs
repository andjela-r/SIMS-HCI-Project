using System;
using InitialProject.Serializer;


namespace InitialProject.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationDays { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int id, int accommodationId, int guestId, DateTime startDate, DateTime endDate, int durationDays)
        {
            this.Id = id;
            this.AccommodationId = accommodationId;
            this.GuestId = guestId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DurationDays = durationDays;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[3]);
            DurationDays = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationId.ToString(), GuestId.ToString(), StartDate.ToString(), EndDate.ToString(), DurationDays.ToString() };
            return csvValues;
        }
    }
}
