using System.Linq;
using System;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Guide : ISerializable
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool SuperStatus { get; set; } = false;
        public bool Resignation { get; set; } = false;
        public Guide() { }
        public Guide(string username, bool superStatus, bool resignation)
        {
            Username = username;
            SuperStatus = superStatus;
            Resignation = resignation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { UserId.ToString(), Username, SuperStatus.ToString(), Resignation.ToString() };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            UserId = Convert.ToInt32(values[0]);
            Username = values[1];
            SuperStatus = Convert.ToBoolean(values[2]);
            Resignation = Convert.ToBoolean(values[3]);
        }
    }
}
