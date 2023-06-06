using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    internal class GuideService
    {
        GuideRepository guideRepository = new GuideRepository();
        VoucherService voucherService = new VoucherService();
        TourService tourService = new TourService();
        TourRepository tourRepository = new TourRepository();
        TourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();

        public void GiveResignation(int guideId)
        {
            guideRepository.ChangeResignationStatus(guideId);
            voucherService.DeleteVoucherBecauseOfGuidesResignation(guideId);
            tourService.DeleteTourBecauseOfGuidesResignation(guideId);
        }

        string GetTourLanguage(int tourId)
        {
            Tour tour = tourRepository.FindById(tourId);
            return tour.Language;
        }

        public bool CheckSuperGuideCriteria(int guideId)
        {
            Guide guide = guideRepository.FindById(guideId);
            bool isSuperGuide = guide.SuperStatus;
            List<TourAppointment> tourAppointments = new List<TourAppointment>();

            DateTime? expirationDate = null;

            if (!isSuperGuide && expirationDate>DateTime.Today)
            {
                List<Tour> tours = tourRepository.FindByGuide(guideId);
                foreach (Tour tour in tours)
                {
                    List<TourAppointment> allTourAppointments = tourAppointmentRepository.FindByTour(tour.Id);
                    foreach (TourAppointment tourAppointment in allTourAppointments)
                    {
                        if (tourAppointment.Status == Status.Finished)
                        {
                            tourAppointments.Add(tourAppointment);
                        }
                    }
                }

                string mostCommonLanguage = tourAppointments
                    .GroupBy(appointment => GetTourLanguage(appointment.TourId))
                    .OrderByDescending(group => group.Count())
                    .Select(group => group.Key)
                    .FirstOrDefault();

                int languageCount = tourAppointments.Count(appointment =>
                {
                    string language = GetTourLanguage(appointment.TourId);
                    return string.Equals(language, mostCommonLanguage, StringComparison.OrdinalIgnoreCase);
                });

                float average = GetGuidesTourRatingAverage(tourRepository.FindByLanguage(mostCommonLanguage));


                if (languageCount > 5 && average>=4.5)
                {
                    guide.SuperStatus = true;
                    guideRepository.Update(guide);
                    expirationDate = DateTime.Now.AddYears(2);
                }
                
            }

            return isSuperGuide;
        }

        public float GetGuidesTourRatingAverage(List<Tour> tours)
        {
            TourRatingRepository tourRatingRepository = new TourRatingRepository();

            List<TourRating> tourRatings = new List<TourRating>();

            foreach (Tour tour in tours)
            {
                tourRatings.AddRange(tourRatingRepository.FindByTour(tour));
            }

            float sumOfRatings = 0;

            foreach (TourRating rating in tourRatings)
            {
                sumOfRatings += rating.GuideKnowledge + rating.GuideLanguage + rating.TourAmusement;
            }

            float n = tourRatings.Count();

            float average = sumOfRatings / n / 3;

            return average;
            
        }
    }
}
