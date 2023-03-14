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
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public List<int> KeyPointsId { get; set; }
        public DateTime StartTime { get; set; }
        public float Duration { get; set; }
        public List<int> PicturesId { get; set; }

        public TourDTO() { }

        public TourDTO(string name, Location location, string description, string language, int maxGuests, List<int> keyPointsId, DateTime startTime, float duration, List<int> picturesId)
        {
            this.Name = name;
            this.Location = location;
            this.Description = description;
            this.Language = language;
            this.MaxGuests = maxGuests;
            this.KeyPointsId = keyPointsId;
            this.StartTime = startTime;
            this.Duration = duration;
            this.PicturesId = picturesId;
        }
    }
}
