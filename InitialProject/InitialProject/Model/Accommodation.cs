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
            int minDays, int cancelPeriod, List<int> picturesId)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = location;
            this.Type = type;
            this.MaxOccupancy = occupancy;
            this.MinDays = minDays;
            this.CancelPeriod = cancelPeriod;
            this.PicturesId = picturesId;
        }

        public Accommodation() { }


        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Name).ToArray();
            csvValues = csvValues.Append(LocationId.ToString()).ToArray();
            int type = Convert.ToInt32(Type);
            csvValues = csvValues.Append(type.ToString()).ToArray();
            csvValues = csvValues.Append(MaxOccupancy.ToString()).ToArray();
            csvValues = csvValues.Append(MinDays.ToString()).ToArray();
            csvValues = csvValues.Append(CancelPeriod.ToString()).ToArray();
            StringBuilder picturesId = new StringBuilder();
            picturesId.AppendJoin(ListDelimiter, PicturesId);
            csvValues = csvValues.Append(picturesId.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = (Type)Convert.ToInt32(values[3]);
            MaxOccupancy = Convert.ToInt32(values[4]);
            MinDays = Convert.ToInt32(values[5]);
            CancelPeriod = Convert.ToInt32(values[6]);
            var picturesId = values[7].Split(ListDelimiter);
            PicturesId = picturesId.Select(Int32.Parse).ToList();   
        }










        /*public void FromCSV(string[] values)
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

            OwnerId = Convert.ToInt32(values[7]);*/

        }
    }
