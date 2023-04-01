using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourDTO
    {
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public List<int> KeyPointsId { get; set; }
        public float Duration { get; set; }
        public List<string> Pictures { get; set; }

        public TourDTO() { }

        public TourDTO(string name, int locationId, string description, string language, int maxGuests, List<int> keyPointsId, float duration, List<string> pictures)
        {
            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxGuests = maxGuests;
            this.KeyPointsId = keyPointsId;
            this.Duration = duration;
            this.Pictures = pictures;
        }
    }
}
