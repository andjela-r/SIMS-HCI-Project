using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class TourRatingService
    {
        TourRatingRepository tourRatingRepository = new TourRatingRepository();

        public void CreateTourRating(TourRatingDTO tourRatingDTO)
        {
            TourRating tourRating = new TourRating();
            tourRating.Id = tourRatingRepository.NextId();
            //TODO TourId set
            tourRating.GuideKnowledge = tourRatingDTO.GuideKnowledge;
            tourRating.GuideLanguage = tourRatingDTO.GuideLanguage;
            tourRating.Comment = tourRatingDTO.Comment;
            tourRating.Pictures = tourRatingDTO.Pictures;

            tourRatingRepository.Save(tourRating);

        }
    }
}
