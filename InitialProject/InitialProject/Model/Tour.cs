using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Tour :ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public List<int> KeyPointsId { get; set; }
        public DateTime StartTime { get; set; }
        public float Duration { get; set; }
        public List<int> PicturesId { get; set; }
        public int GuideId { get; set; }
        public List<int> GuestsId { get; set; }

        public Tour() { }

        public Tour(int id, string name, int locationId, string description, string language, int maxGuests, List<int> keyPointsId, DateTime startTime, float duration, List<int> picturesId, int guideId, List<int> guestId)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxGuests = maxGuests; 
            this.KeyPointsId = keyPointsId;
            this.StartTime = startTime; 
            this.Duration = duration;
            this.PicturesId = picturesId;
            this.GuideId = guideId; 
            this.GuestsId = guestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, Language, MaxGuests.ToString(), KeyPointsId.ToString(), StartTime.ToString(), Duration.ToString(), PicturesId.ToString(), GuideId.ToString(), GuestsId.ToString()  };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            Language = values[4];
            MaxGuests = Convert.ToInt32(values[5]);
            KeyPointsId = new List<int>() { Convert.ToInt32(values[6]) };
            StartTime = Convert.ToDateTime(values[7]);
            Duration = Convert.ToInt32(values[8]);
            PicturesId = new List<int>() { Convert.ToInt32(values[9]) };
            GuideId = Convert.ToInt32(values[10]);
            GuestsId = new List<int>() { Convert.ToInt32(values[11]) }; 

        }
    }
}
