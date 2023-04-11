using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class VoucherDTO
    {
        public string Name { get; set; }
        public int TouristId { get; set; }
        public DateTime ExpirationDate { get; set; }

        public VoucherDTO () {}

        public VoucherDTO(string name, int touristId, DateTime expirationDate)
        {
            Name = name;
            TouristId = touristId;
            ExpirationDate = expirationDate;
        }
    }
}
