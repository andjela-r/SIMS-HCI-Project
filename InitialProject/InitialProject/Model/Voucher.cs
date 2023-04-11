using System;
using System.Linq;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TouristId { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Voucher(){}

        public Voucher(int id, string name, int touristId, DateTime expirationDate)
        {
            Id = id;
            Name = name;
            TouristId = touristId;
            ExpirationDate = expirationDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TouristId = Convert.ToInt32(values[2]);
            ExpirationDate = Convert.ToDateTime(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Name).ToArray();
            csvValues = csvValues.Append(TouristId.ToString()).ToArray();
            csvValues = csvValues.Append(ExpirationDate.ToString()).ToArray();

            return csvValues;
        }
    }
}
