using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitialProject.Model
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Type Type { get; set; }
        public int MaxOccupancy { get; set; }
        public int MinDays { get; set; }
        public int CancelPeriod { get; set; } = 1;
        public List<int> PicturesId { get; set; }

        private readonly string ListDelimiter = ",";

        public Accommodation(int id, string name, int location, Type type, int occupancy,
            int minDays, int cancelPeriod, List<int> pictures)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = location;
            this.Type = type;
            this.MaxOccupancy = occupancy;
            this.MinDays = minDays;
            this.CancelPeriod = cancelPeriod;
            this.PicturesId = pictures;
  
        }

        public Accommodation() { }

        public string[] ToCSV()
        {
            string[] csvValues = new string[9];
            csvValues.Append(Id.ToString());
            csvValues.Append(Name);
            csvValues.Append(LocationId.ToString());
            csvValues.Append(Type.ToString());
            csvValues.Append(MaxOccupancy.ToString());
            csvValues.Append(MinDays.ToString());
            csvValues.Append(CancelPeriod.ToString());

            StringBuilder picturesIds = new StringBuilder();
            picturesIds.AppendJoin(ListDelimiter, PicturesId);
            csvValues.Append(picturesIds.ToString());
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = (Type) Convert.ToInt32(values[3]);
            MaxOccupancy = Convert.ToInt32(values[4]);
            MinDays = Convert.ToInt32(values[5]);
            CancelPeriod = Convert.ToInt32(values[6]);

            var pictureIds = values[7].Split(ListDelimiter);
            PicturesId = pictureIds.Select(Int32.Parse).ToList();

        }
    }
}
