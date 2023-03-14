using System;
using System.Collections.Generic;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public int Cleanliness { get; set; }    
        public int Obedience { get; set; }
        public string Comment { get; set; }

        public GuestRating(int id, int cleanliness, int obedience, string comment)
        {
            this.Id = id;
            this.Cleanliness=cleanliness;
            this.Obedience=obedience;
            this.Comment=comment;
        }

        public GuestRating() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Cleanliness.ToString(), 
            Obedience.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            Obedience = Convert.ToInt32(values[2]);
            Comment = values[3];
        }
    }
}