using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class OwnerAndAccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerHospitality { get; set; }
        public string Comment { get; set; }
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public List<string> Pictures { get; set; }
        public bool IsRated { get; set; }

        private readonly string ListDelimiter = ",";

        public OwnerAndAccommodationRating(int cleanliness, int ownerHospitality, string comment, int ownerId, int accommodationId, List<string> pictures)
        {
            this.Cleanliness = cleanliness;
            this.OwnerHospitality = ownerHospitality;
            this.Comment = comment;
            this.OwnerId = ownerId;
            this.AccommodationId = accommodationId;
            this.Pictures = pictures;
        }

        public OwnerAndAccommodationRating()  {}

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Cleanliness.ToString()).ToArray();
            csvValues = csvValues.Append(OwnerHospitality.ToString()).ToArray();           
            csvValues = csvValues.Append(Comment.ToString()).ToArray();
            csvValues = csvValues.Append(OwnerId.ToString()).ToArray();
            csvValues = csvValues.Append(AccommodationId.ToString()).ToArray();
            StringBuilder pictures = new StringBuilder();
            pictures.AppendJoin(ListDelimiter, Pictures);
            csvValues = csvValues.Append(pictures.ToString()).ToArray();
            bool isRated = Convert.ToBoolean(IsRated);
            csvValues = csvValues.Append(IsRated.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            OwnerHospitality = Convert.ToInt32(values[2]);
            Comment = values[3];
            OwnerId = Convert.ToInt32(values[4]);
            AccommodationId = Convert.ToInt32(values[5]);
            var pictures = values[6].Split(ListDelimiter);
            Pictures = pictures.ToList();
            IsRated = Convert.ToBoolean(values[7]);
        }

    }
}
