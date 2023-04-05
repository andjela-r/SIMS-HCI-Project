using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourRatingDTO
    {
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourAmusement { get; set; }
        public string Comment { get; set; }
        public List<String> Pictures { get; set; }

        public TourRatingDTO() { }

        public TourRatingDTO(int guideKnowledge, int guideLanguage, int tourAmusement, string comment = null, List<string> pictures = null)
        {
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourAmusement = tourAmusement;
            Comment = comment;
            Pictures = pictures;
        }
    }
}
