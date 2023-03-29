using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitialProject.Model
{
    public class Appointment : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartTime { get; set; }
        public int GuideId { get; set; }
        public List<int> TouristsId { get; set; }
        public Status Status { get; set; }  

        private readonly string ListDelimiter = ",";

        public Appointment() { }

        public Appointment(int id, int TourID, DateTime startTime, int guideId/*, List<int> guestsId*/, Status status)
        {
            this.Id = id;
            this.TourId = TourID;
            this.StartTime = startTime;
            this.GuideId = guideId;
            this.TouristsId = null;
            this.Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(TourId.ToString()).ToArray();
            csvValues = csvValues.Append(StartTime.ToString("MM/dd/yyyy HH:mm:ss tt")).ToArray();
            csvValues = csvValues.Append(GuideId.ToString()).ToArray();

            
            StringBuilder guestIds = new StringBuilder();
            
            if(TouristsId != null) 
            {
                guestIds.AppendJoin(ListDelimiter, TouristsId);
                csvValues = csvValues.Append(guestIds.ToString()).ToArray();
            } else
                csvValues = csvValues.Append("").ToArray();

            int status = Convert.ToInt32(Status);
            csvValues = csvValues.Append(status.ToString()).ToArray();

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[1]);
            StartTime = Convert.ToDateTime(values[2]);
            GuideId = Convert.ToInt32(values[3]);

            var guestIds = values[4].Split(ListDelimiter);
            if (guestIds.Select(x => Int32.TryParse(x, out var result)).All(x => x == true))
                TouristsId = guestIds.Select(Int32.Parse).ToList();
            else
                TouristsId = TouristsId;
            
            Status = (Status)Convert.ToInt32(values[5]);
        }
    }
}
