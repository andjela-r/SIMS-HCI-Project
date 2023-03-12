using InitialProject.Model;
using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
	public class KeyPoint : ISerializable
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        public KeyPoint() { }

        public KeyPoint(int id, string name, Status status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Status.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = (Status)Convert.ToInt32(values[2]);
        }

    }
}
