using InitialProject.DTO;
using InitialProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;
using Type = InitialProject.Model.Type;

namespace InitialProject.Service
{
    public class AccommodationService
    {
        AccommodationRepository accommodationRepository = new AccommodationRepository();
        public void CreateAccommodation(AccommodationDTO accommodationDTO)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Name = accommodationDTO.Name;
            accommodation.LocationId = accommodationDTO.LocationId; 
            accommodation.Type = accommodationDTO.Type;
            accommodation.MaxGuests = accommodationDTO.MaxGuests;
            accommodation.MinStay = accommodationDTO.MinStay;
            accommodation.DaysToCancelBeforeReservation = accommodationDTO.DaysToCancelBeforeReservation;
            accommodation.Pictures = accommodationDTO.Pictures;

            accommodationRepository.Save(accommodation);
        }
        private static void AccommodationRegistration()
        {

            int idLocation, type;
            LocationRepository locationRepository = new LocationRepository();
            AccommodationService accommodationService = new AccommodationService();

            Console.WriteLine("Insert accommodation name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Insert location id: ");
            idLocation = Convert.ToInt32(Console.ReadLine());

            while (locationRepository.FindById(idLocation) == null)
            {
                Console.WriteLine("That location does not exist. Try again: ");
                Console.WriteLine("Insert location id: ");
                idLocation = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Insert accommodation type:\n ");
            Console.WriteLine("0-apartment\n1-house\n2-cottage");
            type = Convert.ToInt32(Console.ReadLine());

            while ((Type)type != Type.House && (Type)type != Type.Cottage && (Type)type != Type.Apartment)
            {
                Console.WriteLine("That type does not exist. Try again: ");
                Console.WriteLine("Insert accommodation type:\n ");
                type = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Insert maximum number of guests: ");
            int maxOccupancy = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert minimum stay length (in days): ");
            int minDays = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert cancel period: ");
            int cancelPeriod = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert picture ids: ");
            var pictures = Console.ReadLine().Split(',');
            List<string> pictureLinks = pictures.ToList();

            AccommodationDTO smestaj = new AccommodationDTO(name,
                idLocation, (Type)type, maxOccupancy, minDays, cancelPeriod, pictureLinks);
            accommodationService.CreateAccommodation(smestaj);
        }












    }
}
