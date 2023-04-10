using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<int> KeyPointsId { get; set; }
        public float Duration { get; set; }
        public List<string> Pictures { get; set; }
        public int GuideId { get; set; }

        private readonly string ListDelimiter = ",";

        public Tour() { }

        public Tour(int id, string name, int locationId, string description, string language, int maxTourists, List<int> keyPointsId, float duration, List<string> pictures, int guideId)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxTourists = maxTourists; 
            this.KeyPointsId = keyPointsId;
            this.Duration = duration;
            this.Pictures = pictures;
            this.GuideId = guideId;
        }

        public string[] ToCSV()
        {
            StringBuilder pictures = new StringBuilder();
            pictures.AppendJoin(ListDelimiter, Pictures);

            string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, Language, MaxTourists.ToString(), 
                Duration.ToString(), pictures.ToString(), GuideId.ToString() };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            Language = values[4];
            MaxTourists = Convert.ToInt32(values[5]);

            Duration = float.Parse(values[6]);

            var pictures = values[7].Split(ListDelimiter);
            Pictures = pictures.ToList();

            GuideId = Convert.ToInt32(values[8]);
        }

    }
}
