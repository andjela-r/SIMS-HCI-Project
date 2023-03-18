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
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Type Type { get; set; }
        public int MaxOccupancy { get; set; }
        public int MinDays { get; set; }
        public int CancelPeriod { get; set; } = 1;
        public List<int> PicturesId { get; set; }
        public int OwnerId { get; set; }

        public AccommodationDTO(int id, string name, int locationId, Type type, int occupancy,
          int minDays, int cancelPeriod, List<int> pictures, int ownerId)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
            this.Type = type;
            this.MaxOccupancy = occupancy;
            this.MinDays = minDays;
            this.CancelPeriod = cancelPeriod;
            this.PicturesId = pictures;
            this.OwnerId = ownerId;
        }

        public AccommodationDTO() { }

    }
}
