using InitialProject.Serializer;
using InitialProject.View.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class Renovation: ISerializable
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ExpectedDuration { get; set; } 

        public string Description { get; set; }

        public int AccommodationId { get; set; }

        public Renovation() { } 

        public Renovation(DateTime startDate, DateTime endDate, int expectedDuration, string Description, int accommodationId)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ExpectedDuration = expectedDuration;
            this.Description = Description;
            this.AccommodationId = accommodationId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(StartDate.ToString()).ToArray();
            csvValues = csvValues.Append(EndDate.ToString()).ToArray();
            csvValues = csvValues.Append(ExpectedDuration.ToString()).ToArray();
            csvValues = csvValues.Append(Description).ToArray();
            csvValues = csvValues.Append(AccommodationId.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = Convert.ToDateTime(values[1]);
            EndDate = Convert.ToDateTime(values[2]);
            ExpectedDuration = Convert.ToInt32(values[3]);
            Description = values[4];
            AccommodationId = Convert.ToInt32(values[5]);
        }














    }
}
