using System;
using System.Linq;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Voucher(){ }

        public Voucher(int id, int touristId, string name, DateTime expirationDate)
        {
            Id = id;
            TouristId = touristId;
            Name = name;
            ExpirationDate = expirationDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), Name, ExpirationDate.ToString("MM/dd/yyyy HH:mm:ss tt") };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Name = values[2];
            ExpirationDate = Convert.ToDateTime(values[3]);
        }
    }
}
