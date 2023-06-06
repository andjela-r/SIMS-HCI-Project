using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public int LocationIntId { get; set; }
        public bool IsOpen { get; set; }
        public int GuestId { get; set; }
        public string Questions { get; set; }
        public string Symbol { get; set; }
        public bool WasThere { get; set; }
        public int GuestCommentNumber { get; set; }
        public int OwnerCommentNumber { get; set; }

        public Forum() { }

        public Forum(string questions, int guestId, int locationIntId, bool wasThere, bool isOpen, int guestNumber)
        {
            Questions = questions;
            GuestId = guestId;
            LocationIntId = locationIntId;
            WasThere = wasThere;
            IsOpen = isOpen;
            GuestCommentNumber = guestNumber;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationIntId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            IsOpen = Convert.ToBoolean(values[3]);
            Questions = Convert.ToString(values[4]);
            WasThere = Convert.ToBoolean(values[5]);
            GuestCommentNumber = Convert.ToInt32(values[0]);
            OwnerCommentNumber = Convert.ToInt32(values[0]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] {};
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(LocationIntId.ToString()).ToArray();
            csvValues = csvValues.Append(GuestId.ToString()).ToArray();
            csvValues = csvValues.Append(IsOpen.ToString()).ToArray();
            csvValues = csvValues.Append(Questions.ToString()).ToArray();
            csvValues = csvValues.Append(WasThere.ToString()).ToArray();
            csvValues = csvValues.Append(GuestCommentNumber.ToString()).ToArray();
            csvValues = csvValues.Append(OwnerCommentNumber.ToString()).ToArray();
            return csvValues;
        }

    }
}
