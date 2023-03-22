using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitialProject.Model
{
	public class KeyPoint : ISerializable
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<int> ArrivedIds { get; set; }

        private readonly string ListDelimiter = ",";

        public KeyPoint() { }

        public KeyPoint(int id, string name, Status status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
            this.ArrivedIds = null;
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] { };
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(Name).ToArray();

            int status = Convert.ToInt32(Status);
            csvValues = csvValues.Append(status.ToString()).ToArray();

            StringBuilder arrivedIds = new StringBuilder();

            if (ArrivedIds != null)
            {
                arrivedIds.AppendJoin(ListDelimiter, ArrivedIds);
                csvValues = csvValues.Append(arrivedIds.ToString()).ToArray();
            }
            else
                csvValues = csvValues.Append("").ToArray();

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = (Status)Convert.ToInt32(values[2]);

            var arrivedIds = values[3].Split(ListDelimiter);
            if (arrivedIds.Select(x => Int32.TryParse(x, out var result)).All(x => x == true))
                ArrivedIds = arrivedIds.Select(Int32.Parse).ToList();
            else
                ArrivedIds = ArrivedIds;
        }

    }
}
