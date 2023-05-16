using System.Linq;
using System;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Guest : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsSuperGuest { get; set; }
        public int BonusPoints { get; set; }

        public Guest() { }

        public Guest(int userId, string username, string password, bool status, int bonusPoints)
        {
            UserId = userId;
            Username = username;
            Password = password;
            IsSuperGuest = status;
            BonusPoints = bonusPoints;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Username, Password, IsSuperGuest.ToString(), BonusPoints.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Username = values[2];
            Password = values[3];
            IsSuperGuest = Convert.ToBoolean(values[4]);
            BonusPoints = Convert.ToInt32(values[5]);
        }
    }
}
