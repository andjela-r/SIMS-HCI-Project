using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public List<int> PartIds { get; set; }
        public RequestStatusEnum TotalStatus { get; set; } 

        private readonly string ListDelimiter = ",";

        public ComplexTourRequest() { }

        public ComplexTourRequest(int id, int touristId, List<int> partIds, RequestStatusEnum totalStatus = RequestStatusEnum.Waiting)
        {
            this.Id = id;
            this.TouristId = touristId;
            this.PartIds = partIds;
            this.TotalStatus = totalStatus;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            var partIds = values[2].Split(ListDelimiter);
            if (partIds.Select(x => Int32.TryParse(x, out var result)).All(x => x == true))
                PartIds = partIds.Select(Int32.Parse).ToList();
            else
                PartIds = PartIds;
            TotalStatus = (RequestStatusEnum)Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString()};

            StringBuilder partIds = new StringBuilder();
            if (PartIds != null)
            {
                partIds.AppendJoin(ListDelimiter, PartIds);
                csvValues = csvValues.Append(partIds.ToString()).ToArray();
            }
            else
                csvValues = csvValues.Append("").ToArray();

            csvValues = csvValues.Append(Convert.ToInt32(TotalStatus).ToString()).ToArray();

            return csvValues;
        }
    }
}
