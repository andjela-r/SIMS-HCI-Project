using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class RenovationSuggestion : ISerializable
    {
        public int Id;
        public int ReservationId;
        public int Level;
        public string Comment;

        public RenovationSuggestion() {   }
        
        public RenovationSuggestion(int reservationId, int level, string comment)
        {
            this.ReservationId = reservationId;
            this.Level = level;
            this.Comment = comment;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Level = Convert.ToInt32(values[2]);
            Comment = Convert.ToString(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Level.ToString(), Comment.ToString()};
            return csvValues;
        }

    }
}
