using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        Location Location { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        public List<int> KeyPointsId { get; set; }
        DateTime StartTime { get; set; }
        public float Duration { get; set; }
        public List<int> PicturesId { get; set; }
        public int GuideId { get; set; }
        public List<int> GuestsId { get; set; }

        public Tour() { }

        public Tour(int id, string name, Location location, string description, int maxGuests, List<int> keyPointsId, DateTime startTime, float duration, List<int> picturesId, int guideId, List<int> guestId)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.Description = description;
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
            string[] csvValues = { Id.ToString(), Name, Location.ToString(), Description, MaxGuests.ToString(), KeyPointsId.ToString(), StartTime.ToString(), Duration.ToString(), PicturesId.ToString(), GuideId.ToString(), GuestsId.ToString()  };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Description = values[3];
            MaxGuests = Convert.ToInt32(values[4]);
            KeyPointsId = new List<int>() { Convert.ToInt32(values[5]) };
            StartTime = Convert.ToDateTime(values[6]);
            Duration = Convert.ToInt32(values[7]);
            PicturesId = new List<int>() { Convert.ToInt32(values[8]) };
            GuideId = Convert.ToInt32(values[9]);
            GuestsId = new List<int>() { Convert.ToInt32(values[10]) }; 

        }
    }
}
