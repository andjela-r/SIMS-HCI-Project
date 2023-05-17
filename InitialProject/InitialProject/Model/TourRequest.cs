using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.View.Tourist;

namespace InitialProject.Model
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public RequestStatusEnum Status { get; set; }

        public TourRequest() { }

        public TourRequest (int touristId, int locationId, string description, string language, int maxTourists, DateTime startDate, DateTime endDate, RequestStatusEnum status)
        {
            this.TouristId = touristId;
            this.LocationId = locationId;
            this.Description = description;
            this.Language = language;
            this.MaxTourists = maxTourists;
            this.StartDate = startDate; 
            this.EndDate = endDate;
            this.Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), LocationId.ToString(), Description, Language, MaxTourists.ToString(), 
                StartDate.ToString("MM/dd/yyyy HH:mm:ss tt"), EndDate.ToString("MM/dd/yyyy HH:mm:ss tt"), Convert.ToInt32(Status).ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            Language = values[4];
            MaxTourists = Convert.ToInt32(values[5]);
            StartDate = Convert.ToDateTime(values[6]);
            EndDate = Convert.ToDateTime(values[7]);
            Status = (RequestStatusEnum)Convert.ToInt32(values[8]);
        }
    }
}
