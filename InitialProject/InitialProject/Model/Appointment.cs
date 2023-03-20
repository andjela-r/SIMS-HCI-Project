using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Appointment : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public int GuideId { get; set; }
        public List<int> GuestsId { get; set; }
        public Status Status { get; set; }  

        private readonly string ListDelimiter = ",";

        public Appointment() { }

        public Appointment(int id, int TourID, DateTime startTime, int guideId, List<int> guestsId, Status status)
        {
            this.Id = id;
            this.TourId = TourID;
            this.StartTime = startTime;
            this.GuideId = guideId;
            this.GuestsId = guestsId;
            this.Status = status;
        }

        public string[] ToCSV()
        {
            /*StringBuilder guestIds = new StringBuilder("");
            if (guestIds != null)
            {
                guestIds.AppendJoin(ListDelimiter, GuestsId);
            } else
            {
                guestIds.Append("");
            }*/

            StringBuilder guestsIds = new StringBuilder();
            guestsIds.AppendJoin(ListDelimiter, GuestsId);

            int status = Convert.ToInt32(Status);

            string[] csvValues = { Id.ToString(), TourId.ToString(), StartTime.ToString("MM/dd/yyyy HH:mm"),  GuideId.ToString(), guestsIds.ToString(), status.ToString()};

            return csvValues;
        }

        public Status GetStatus()
        {
            return Status;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            StartTime = Convert.ToDateTime(values[2]);
            GuideId = Convert.ToInt32(values[3]);

            var guestIds = values[4].Split(ListDelimiter);
            GuestsId = guestIds.Select(Int32.Parse).ToList();
            /*if (guestIds.Length == 0)
            {
                GuestsId = guestIds.Select(Int32.Parse).ToList();
            }
            else
            {
                guestIds.Append("");
            }*/

            Status = (Status)Convert.ToInt32(values[5]);
        }
    }
}
