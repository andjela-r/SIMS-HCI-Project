using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class Location: ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Location(int id, string city, string county)
        {
            this.Id = id;
            this.City = city;
            this.Country = county;
        }

        public Location() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), City, Country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }

}