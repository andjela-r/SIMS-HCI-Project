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
        public int GuestNumber { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate, int durationDays, int guestNumber)
        {
            this.AccommodationId = accommodationId;
            this.GuestId = guestId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DurationDays = durationDays;
            this.GuestNumber = guestNumber;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]);
            DurationDays = Convert.ToInt32(values[5]);
            GuestNumber = Convert.ToInt32(values[6]);

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationId.ToString(), GuestId.ToString(), StartDate.ToString(), EndDate.ToString(), DurationDays.ToString(), GuestNumber.ToString() };
            return csvValues;
        }
    }
}
