using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class TourRating : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourAmusement { get; set; }
        public string Comment { get; set; }
        public List<string> Pictures { get; set; }

        private readonly string ListDelimiter = ",";

        public TourRating() { }

        public TourRating(int id, int tourId, int guideKnowledge, int guideLanguage, int tourAmusement, string comment, List<string> pictures)
        {
            Id = id;
            TourId = tourId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourAmusement = tourAmusement;
            Comment = comment;
            Pictures = pictures;
        }
        public string[] ToCSV()
        {
            StringBuilder pictures = new StringBuilder();
            pictures.AppendJoin(ListDelimiter, Pictures);

            string[] csvValues =
                { Id.ToString(), TourId.ToString(), GuideLanguage.ToString(), 
                    Comment, pictures.ToString() };
                return csvValues;
        }

        public void FromCSV(string[] values)
        {
           Id = Convert.ToInt32(values[0]);
           TourId = Convert.ToInt32(values[1]);
           GuideKnowledge = Convert.ToInt32(values[2]);
           GuideLanguage = Convert.ToInt32(values[3]);
           TourAmusement = Convert.ToInt32(values[4]);
           Comment = values[5];

           var pictures = values[8].Split(ListDelimiter);
           Pictures = pictures.ToList();

        }

        
    }
}