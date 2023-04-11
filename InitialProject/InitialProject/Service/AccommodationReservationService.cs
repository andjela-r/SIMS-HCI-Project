using System.Collections.Generic;
using System;
using System.DirectoryServices;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System.Linq;

namespace InitialProject.Service
{
    public class AccommodationReservationService
    {
        AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();

        public void CreateAccommodationReservation(AccommodationReservationDTO accommodationReservationDTO)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation();
            accommodationReservation.AccommodationId = accommodationReservationDTO.AccommodationId;
            accommodationReservation.GuestId = accommodationReservationDTO.GuestId;
            accommodationReservation.StartDate = accommodationReservationDTO.StartDate;
            accommodationReservation.EndDate = accommodationReservationDTO.EndDate;
            accommodationReservation.DurationDays = accommodationReservationDTO.DurationDays;

            accommodationReservationRepository.Save(accommodationReservation);

        }

        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;
        }

        public List<DateTime> GetOccupiedDays(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = GetDatesBetween(startDate, endDate).ToList();
            return dates;
        }

        public bool IsContainingNameWords(Accommodation accommodation, string[] nameWords)
        {
            bool containsAllWords = true;
            foreach (string word in nameWords)
            {
                if (!accommodation.Name.ToLower().Contains(word))
                {
                    containsAllWords = false;
                    break;
                }
            }
            return containsAllWords;


        }

        public bool HasMatchingAccommodationType(Accommodation accommodation, string type)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(type))
            {
                result = accommodation.Type.ToString().ToLower().Contains(type.ToLower());
            }

            return result;
        }

        public bool IsGuestNumberLessThanMaximum(Accommodation accommodation, string guestNumber)
        {
            bool isLess = false;
            if (int.TryParse(guestNumber, out int parsedGuestNumber) && parsedGuestNumber <= accommodation.MaxGuests)
            {
                isLess = true;
            }

            return isLess;
        }

        public bool IsReservationGreaterThanMinimum(Accommodation accommodation, string daysForReservation)
        {
            bool isGreater = false;
            if (int.TryParse(daysForReservation, out int parsedDaysForReservation) &&
                parsedDaysForReservation >= accommodation.DaysToCancelBeforeReservation)
            {
                isGreater = true;
            }
            return isGreater;
        }
    }
}
