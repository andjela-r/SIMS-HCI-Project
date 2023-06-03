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
        public Location LocationId { get; set; }
        public int LocationIntId { get; set; }
        public bool WasHere { get; set; }
        public List<string> Questions { get; set; }

        private readonly string ListDelimiter = ",";

        public Forum() { }

        public Forum(List<string> questions)
        {
            Questions = questions;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationIntId = Convert.ToInt32(values[1]);
            WasHere = Convert.ToBoolean(values[2]);
            var questions = values[3].Split(ListDelimiter);
            Questions = questions.ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = new string[] {};
            csvValues = csvValues.Append(Id.ToString()).ToArray();
            csvValues = csvValues.Append(LocationIntId.ToString()).ToArray();
            csvValues = csvValues.Append(WasHere.ToString()).ToArray();
            StringBuilder questions = new StringBuilder();
            questions.AppendJoin(ListDelimiter, Questions);
            csvValues = csvValues.Append(questions.ToString()).ToArray();
            return csvValues;
        }

    }
}
