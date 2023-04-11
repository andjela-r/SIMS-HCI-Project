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
        public Location Location { get; set; }
        public int MaxGuests { get; set; }
        public int MinStay { get; set; }
        public int OwnerId { get; set; }
        public int DaysToCancelBeforeReservation { get; set; } = 1;
        public List<string> Pictures { get; set; }

        private readonly string ListDelimiter = ",";

        public Accommodation(int id, string name, int location, Type type, int guests,
            int minStay, int daysToCancelBeforeReservation, List<string> pictures)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = location;
            this.Type = type;
            this.MaxGuests = guests;
            this.MinStay = minStay;
            this.DaysToCancelBeforeReservation = daysToCancelBeforeReservation;
            this.Pictures = pictures;
        }

        public Accommodation()
        {
        }


        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Name).ToArray();
            csvValues = csvValues.Append(LocationId.ToString()).ToArray();
            int type = Convert.ToInt32(Type);
            csvValues = csvValues.Append(type.ToString()).ToArray();
            csvValues = csvValues.Append(MaxGuests.ToString()).ToArray();
            csvValues = csvValues.Append(MinStay.ToString()).ToArray();
            csvValues = csvValues.Append(DaysToCancelBeforeReservation.ToString()).ToArray();
            csvValues = csvValues.Append(OwnerId.ToString()).ToArray();
            StringBuilder pictures = new StringBuilder();
            pictures.AppendJoin(ListDelimiter, Pictures);
            csvValues = csvValues.Append(pictures.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = (Type)Convert.ToInt32(values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinStay = Convert.ToInt32(values[5]);
            DaysToCancelBeforeReservation = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
            var pictures = values[8].Split(ListDelimiter);
            Pictures = pictures.ToList();
        }










        /*public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = (Type) Convert.ToInt32(values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinStay = Convert.ToInt32(values[5]);
            DaysToCancelBeforeReservation = Convert.ToInt32(values[6]);

            var pictureIds = values[7].Split(ListDelimiter);
            Pictures = pictureIds.Select(Int32.Parse).ToList();

            OwnerId = Convert.ToInt32(values[7]);

        }*/
    }
}
