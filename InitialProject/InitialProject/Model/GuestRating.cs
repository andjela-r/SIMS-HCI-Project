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
        public int GuestId { get; set; }

        public GuestRating(int id, int cleanliness, int obedience, string comment, int guestId)
        {
            this.Id = id;
            this.Cleanliness=cleanliness;
            this.Obedience=obedience;
            this.Comment=comment;
            this.GuestId = guestId;
        }

        public GuestRating() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Cleanliness.ToString(), 
            Obedience.ToString(), Comment, GuestId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            Obedience = Convert.ToInt32(values[2]);
            Comment = values[3];
            GuestId = Convert.ToInt32(values[4]);   
        }
    }
}