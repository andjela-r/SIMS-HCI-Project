using System;
using System.Collections.Generic;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Accommodation : ISerializable
    {
        public int Id;
        public string Name { get; set; }
        public Location Location { get; set; }
        public Type Type { get; set; }
        public int MaxOccupancy { get; set; }
        public int MinDays { get; set; }
        public int CancelPeriod { get; set; }
        public List<int> Pictures{ get; set; }

        public Accommodation(int id, string name, Location location, Type type, int occupancy,
            int minDays, int cancelPeriod, List<int> pictures)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.Type = type;
            this.MaxOccupancy = occupancy;
            this.MinDays = minDays;
            this.CancelPeriod = cancelPeriod;
            this.Pictures = pictures;
        }

        public Accommodation() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.ToString(), Type.ToString(),
            MaxOccupancy.ToString(), MinDays.ToString(), CancelPeriod.ToString(), Pictures.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Type = (Type) Convert.ToInt32(values[3]);
            MaxOccupancy = Convert.ToInt32(values[4]);
            MinDays = Convert.ToInt32(values[5]);
            CancelPeriod = Convert.ToInt32(values[6]);
            Pictures= new List<int>() { Convert.ToInt32(values[7]) };
        }
    }
}
