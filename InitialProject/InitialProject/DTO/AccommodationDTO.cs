using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = InitialProject.Model.Type;

namespace InitialProject.DTO
{
    public class AccommodationDTO
    {
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Type Type { get; set; }   
        public int MaxGuests { get; set; }
        public int MinStay { get; set; }
        public int DaysToCancelBeforeReservation { get; set; } = 1;
        public List<string> Pictures { get; set; }
     
        public AccommodationDTO(string name, int locationId, Type type, int guests,
         int minStay, int daysToCancelBeforeReservation, List<string> pictures)
        {
            this.Name = name;
            this.LocationId = locationId;
            this.Type = type;
            this.MaxGuests = guests;
            this.MinStay = minStay;
            this.DaysToCancelBeforeReservation = daysToCancelBeforeReservation;
            this.Pictures = pictures;
        }

        public AccommodationDTO() { }

    }
}
