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
        public int MaxGuests { get; set; }
        public List<int> KeyPointsId { get; set; }
        public float Duration { get; set; }
        public List<int> PicturesId { get; set; }

        private readonly string ListDelimiter = ",";

        public Tour() { }

        public Tour(int id, string name, int locationId, string description, string language, int maxGuests, List<int> keyPointsId, float duration, List<int> picturesId)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxGuests = maxGuests; 
            this.KeyPointsId = keyPointsId;
            this.Duration = duration;
            this.PicturesId = picturesId;
        }

        public string[] ToCSV()
        {
            StringBuilder keyPoints = new StringBuilder();
            keyPoints.AppendJoin(ListDelimiter, KeyPointsId);

            StringBuilder pictureIds = new StringBuilder();
            pictureIds.AppendJoin(ListDelimiter, PicturesId);

            string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, Language, MaxGuests.ToString(), 
                keyPoints.ToString(), Duration.ToString(), pictureIds.ToString() };
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

            var keyPoints = values[6].Split(ListDelimiter);
            KeyPointsId = keyPoints.Select(int.Parse).ToList();

            Duration = float.Parse(values[7]);

            var pictureIds = values[8].Split(ListDelimiter);
            PicturesId = pictureIds.Select(int.Parse).ToList();
        }

    }
}
