using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class AccommodationReservationDTO
    {
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationDays { get; set; }

        public AccommodationReservationDTO() { }

        public AccommodationReservationDTO(int accommodationId, int guestId, DateTime startDate, DateTime endDate, int durationDays)
        {
            this.AccommodationId = accommodationId;
            this.GuestId = guestId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DurationDays = durationDays;
        }

       
    }
}
