using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public DateTime StartTime { get; set; }
        public float Duration { get; set; }
        public List<int> PicturesId { get; set; }
        public int GuideId { get; set; }
        public List<int> GuestsId { get; set; }

        private readonly string ListDelimiter = ",";

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
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Name).ToArray();
            csvValues = csvValues.Append(LocationId.ToString()).ToArray();
            csvValues = csvValues.Append(Description).ToArray();
            csvValues = csvValues.Append(Language.ToString()).ToArray();
            csvValues = csvValues.Append(MaxGuests.ToString()).ToArray();

            StringBuilder keyPoints = new StringBuilder();
            keyPoints.AppendJoin(ListDelimiter, KeyPointsId);

            csvValues = csvValues.Append(keyPoints.ToString()).ToArray();
            csvValues = csvValues.Append(StartTime.ToString("MM/dd/yyyy HH:mm:ss tt")).ToArray();
            csvValues = csvValues.Append(Duration.ToString()).ToArray();

            StringBuilder pictureIds = new StringBuilder();
            pictureIds.AppendJoin(ListDelimiter, PicturesId);
            csvValues = csvValues.Append(pictureIds.ToString()).ToArray();

            csvValues = csvValues.Append(GuideId.ToString()).ToArray();

            StringBuilder guestIds = new StringBuilder();
            guestIds.AppendJoin(ListDelimiter, GuestsId);
            csvValues = csvValues.Append(guestIds.ToString()).ToArray();

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
            KeyPointsId = keyPoints.Select(Int32.Parse).ToList();

            StartTime = Convert.ToDateTime(values[7]);
            Duration = float.Parse(values[8]);

            var pictureIds = values[9].Split(ListDelimiter);
            PicturesId = pictureIds.Select(Int32.Parse).ToList();

            GuideId = Convert.ToInt32(values[10]);

            var guestIds = values[11].Split(ListDelimiter);
            if (guestIds.Select(x => Int32.TryParse(x, out var result)).All(x => x == true))
                GuestsId = guestIds.Select(Int32.Parse).ToList();
            else
                GuestsId = GuestsId;
        }
    }
}
