using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitialProject.Model
{
    public class TourAppointment : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartTime { get; set; }
        public List<int> TouristIds { get; set; }
        public Status Status { get; set; }
        public List<int> KeyPointIds { get; set; }
        public int AvailableSeats { get; set; }

        private readonly string ListDelimiter = ",";

        public TourAppointment() { }

        public TourAppointment(int id, int tourId, DateTime startTime, Status status, List<int> keyPointIds, int availableSeats)
        {
            this.Id = id;
            this.TourId = tourId;
            this.StartTime = startTime;
            this.TouristIds = null;
            this.Status = status;
            this.KeyPointIds = keyPointIds;
            this.AvailableSeats = availableSeats;
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(TourId.ToString()).ToArray();
            csvValues = csvValues.Append(StartTime.ToString("MM/dd/yyyy HH:mm:ss tt")).ToArray();


            StringBuilder guestIds = new StringBuilder();
            
            if(TouristIds != null) 
            {
                guestIds.AppendJoin(ListDelimiter, TouristIds);
                csvValues = csvValues.Append(guestIds.ToString()).ToArray();
            } else
                csvValues = csvValues.Append("").ToArray();

            int status = Convert.ToInt32(Status);
            csvValues = csvValues.Append(status.ToString()).ToArray();

            StringBuilder keyPoints = new StringBuilder();
            keyPoints.AppendJoin(ListDelimiter, KeyPointIds);
            csvValues = csvValues.Append(keyPoints.ToString()).ToArray();
            csvValues = csvValues.Append(AvailableSeats.ToString()).ToArray();

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            StartTime = Convert.ToDateTime(values[2]);

            var guestIds = values[3].Split(ListDelimiter);
            if (guestIds.Select(x => Int32.TryParse(x, out var result)).All(x => x == true))
                TouristIds = guestIds.Select(Int32.Parse).ToList();
            else
                TouristIds = TouristIds;
            
            Status = (Status)Convert.ToInt32(values[4]);

            var keyPoints = values[5].Split(ListDelimiter);
            KeyPointIds = keyPoints.Select(int.Parse).ToList();
            AvailableSeats = Convert.ToInt32(values[6]);
        }
    }
}
