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
        public int MaxTourists { get; set; }
        public List<int> KeyPointsId { get; set; }
        public float Duration { get; set; }
        public List<string> Pictures { get; set; }
        public int GuideId { get; set; }

        public TourDTO() { }

        public TourDTO(string name, int locationId, string description, string language, int maxTourists, float duration, List<string> pictures/*, int guideId*/)
        {
            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxTourists = maxTourists;
            this.Duration = duration;
            this.Pictures = pictures;
            //this.GuideId = guideId;
        }
    }
}
