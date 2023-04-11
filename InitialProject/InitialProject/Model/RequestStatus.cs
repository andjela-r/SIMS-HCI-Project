using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class RequestStatus : ISerializable
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public RequestStatusEnum Request { get; set; } 

        public int ReservationId { get; set; }

        public string Comment { get; set; }

        public RequestStatus(DateTime startDate, DateTime endDate, string comment, RequestStatusEnum request, int reservationId)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Request = request;
            this.ReservationId = reservationId;
            this.Comment = comment;
        }

        public RequestStatus() { }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(StartDate.ToString()).ToArray();
            csvValues = csvValues.Append(EndDate.ToString()).ToArray();
            int request = Convert.ToInt32(Request);
            csvValues = csvValues.Append(request.ToString()).ToArray();
            csvValues = csvValues.Append(ReservationId.ToString()).ToArray();
            csvValues = csvValues.Append(Comment).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = Convert.ToDateTime(values[1]);
            EndDate = Convert.ToDateTime(values[2]);
            Request = (RequestStatusEnum)Convert.ToInt32(values[3]);
            ReservationId = Convert.ToInt32(values[4]);
            Comment = values[5];    
        }


    }
}
