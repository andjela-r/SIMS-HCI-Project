using InitialProject.Serializer;
using System;
using System.Linq;

namespace InitialProject.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User() { }

        public User(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password};
            var role = Convert.ToInt32(Role);
            csvValues = csvValues.Append(role.ToString()).ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = (Role)Convert.ToInt32(values[3]);
        }
    }
}
