using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
	public class Picture : ISerializable
	{
		public int Id { get; set; }
		public string Link { get; set; }

		public Picture() { }

        public Picture(int id, string link)
        {
            this.Id = id;
            this.Link = link;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Link };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Link = values[1];
        }
    }
}
