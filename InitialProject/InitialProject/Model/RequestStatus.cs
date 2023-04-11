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
        public RequestStatusEnum Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReservationId { get; set; }
        public string Comment { get; set; }

        public RequestStatus(RequestStatusEnum status, int reservationId, DateTime startDate, DateTime endDate, string comment)
        {
            this.Status = status;
            this.ReservationId = reservationId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Comment = comment;
        }

        public RequestStatus() { }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            int status = Convert.ToInt32(Status);
            csvValues = csvValues.Append(status.ToString()).ToArray();
            csvValues = csvValues.Append(ReservationId.ToString()).ToArray();
            csvValues = csvValues.Append(StartDate.ToString()).ToArray();
            csvValues = csvValues.Append(EndDate.ToString()).ToArray();
            csvValues = csvValues.Append(Comment.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Status = (RequestStatusEnum)Convert.ToInt32(values[1]);
            ReservationId = Convert.ToInt32(values[2]);
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]);
            Comment = values[5];    
        }
    }
}
