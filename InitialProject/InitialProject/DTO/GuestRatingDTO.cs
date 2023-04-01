using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.DTO
{
    public class GuestRatingDTO
    {
        public int Cleanliness { get; set; }
        public int Obedience { get; set; }
        public string Comment { get; set; }
        public int GuestId { get; set; }    

        public GuestRatingDTO(int cleanliness, int obedience, string comment, int guestId)
        {
            this.Cleanliness = cleanliness;
            this.Obedience = obedience;
            this.Comment = comment;
            this.GuestId = guestId;
        }

        public GuestRatingDTO() { }

}
}
