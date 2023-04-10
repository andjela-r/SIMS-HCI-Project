using InitialProject.Serializer;
using System;

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

        public OwnerAndAccommodationRating(int cleanliness, int ownerHospitality, string comment, int ownerId, int accommodationId)
        {
            this.Cleanliness = cleanliness;
            this.OwnerHospitality = ownerHospitality;
            this.Comment = comment;
            this.OwnerId = ownerId;
            this.AccommodationId = accommodationId;
        }

        public OwnerAndAccommodationRating()  {}

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Cleanliness.ToString(), OwnerHospitality.ToString(), Comment, OwnerId.ToString(), AccommodationId.ToString() };
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
        }

    }
}
