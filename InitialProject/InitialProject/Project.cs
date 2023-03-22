using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Type = InitialProject.Model.Type;

namespace InitialProject
{

    internal class Program
    {
        [STAThreadAttribute]
        public static void Main()
        {
            string chosenOption;
            do
            {
                WriteMenuOptions();
                chosenOption = Console.ReadLine();
                Console.Clear();
                ProcessChosenOption(chosenOption);
            } while (!chosenOption.Equals("x"));
        }

        private static void WriteMenuOptions()
        {
            Console.WriteLine("1. Accomodation registration");
            Console.WriteLine("2. Rate a guest");
            Console.WriteLine("\n3. Search Accomodation");
            Console.WriteLine("4. Accomodation reservation");
            Console.WriteLine("\n5. Tour registration");
            Console.WriteLine("6. Track a tour");
            Console.WriteLine("\n7. Search Tour");
            Console.WriteLine("8. Tour reservation");
            Console.WriteLine("\nx. Exit");
            Console.Write("Your option: ");
        }

        private static void WriteTourSearchOptions()
        {
            Console.WriteLine("1. Search by location");
            Console.WriteLine("2. Search by duration");
            Console.WriteLine("3. Search by language");
            Console.WriteLine("4. Search by number of guests");
            Console.WriteLine("\nx. Exit");
            Console.Write("Your option: ");
        }


        private static void ProcessChosenOption(string chosenOption)
        {
            switch (chosenOption)
            {
                case "1":
                    do
                    {
                        AccommodationRegistration();
                    } while (!chosenOption.Equals("x"));

                    break;
                case "2":
                    do
                    {
                        RateAGuest();
                    } while (!chosenOption.Equals("x"));

                    break;
                case "3":
                    Console.WriteLine("Option 3\n");
                    break;
                case "4":
                    Console.WriteLine("Option 4\n");
                    break;
                case "5":
                    string creationOption;
                    do
                    {
                        WriteTourCreationOptions();
                        creationOption = Console.ReadLine();
                        Console.Clear();

                        ProcessCreationOption(creationOption);
                    } while (!creationOption.Equals("x"));

                    break;
                case "6":
                    TourService tourService = new TourService();
                    tourService.TrackTourLive();

                    break;
                case "7":
                    string searchOption;
                    do
                    {
                        WriteTourSearchOptions();
                        searchOption = Console.ReadLine();
                        Console.Clear();
                        ProcessSearchTourOption(searchOption);
                    } while (!searchOption.Equals("x"));

                    break;
                case "8":
                    Console.WriteLine("Enter tour id: ");
                    var tourId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter guest id: ");
                    var guestId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter number of guests: ");
                    var guestNumber = Convert.ToInt32(Console.ReadLine());

                    var newReservation = new TourReservation(guestNumber, guestId, tourId);
                    ProcessCreateTourReservation(newReservation);

                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }

        private static void WriteTourCreationOptions()
        {
            Console.WriteLine("1. Tour creation");
            Console.WriteLine("2. Appointment creation");
            Console.WriteLine("3. Key point creation");
            Console.WriteLine("x. Exit");

            Console.WriteLine("Your option: ");
        }

        private static TourDTO GetTourCreationData()
        {
            Console.WriteLine("Insert name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Insert location id: ");
            var locationId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert description: ");
            var description = Console.ReadLine();

            Console.WriteLine("Insert language: ");
            var language = Console.ReadLine();

            Console.WriteLine("Insert max number of guests: ");
            var maxGuests = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert key points: ");
            var keyPoints = Console.ReadLine().Split(',');
            var keyPointsId = keyPoints.Select(int.Parse).ToList();

            Console.WriteLine("Insert duration: ");
            var duration = float.Parse(Console.ReadLine());

            Console.WriteLine("Insert picture ids: ");
            var pictures = Console.ReadLine().Split(',');
            var pictureIds = pictures.Select(int.Parse).ToList();

            var tourDTO = new TourDTO(name, locationId, description, language, maxGuests, keyPointsId, duration,
                pictureIds);

            return tourDTO;
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
            List<int> pictureIds = pictures.Select(Int32.Parse).ToList();

            AccommodationDTO smestaj = new AccommodationDTO(name,
                idLocation, (Type)type, maxOccupancy, minDays, cancelPeriod, pictureIds);
            accommodationService.CreateAccommodation(smestaj);
        }

        private static void RateAGuest()
        {
            /* this will be activated with front
             AccommodationReservation accommodationReservation = new AccommodationReservation();
             if (!isDeadlineOver(accommodationReservation))
             {
                 Console.WriteLine("You have not rated a guest yet.");
             } else Console.WriteLine("You have already rated a guest.");*/

            int cleanliness, obedience;
            GuestRatigService guestRatigService = new GuestRatigService();

            Console.WriteLine("Rate cleanliness 1-5: ");
            cleanliness = Convert.ToInt32(Console.ReadLine());

            while (cleanliness > 5 || cleanliness < 1)
            {
                Console.WriteLine("Invalid rating. Try again, 1-5: ");
                Console.WriteLine("Rate cleanliness 1-5: ");
                cleanliness = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Rate obedience 1-5: ");
            obedience = Convert.ToInt32(Console.ReadLine());

            while (obedience > 5 || obedience < 1)
            {
                Console.WriteLine("Invalid rating. Try again, 1-5: ");
                Console.WriteLine("Rate obedience 1-5: ");
                obedience = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Insert comment: ");
            string comment = Console.ReadLine();

            Console.WriteLine("Insert guest id: ");
            int guestId = Convert.ToInt32(Console.ReadLine());

            GuestRatingDTO guestRatingDTO = new GuestRatingDTO(
                cleanliness, obedience, comment, guestId);
            guestRatigService.CreateGuestRating(guestRatingDTO);
        }

        private static bool IsDeadlineOver(AccommodationReservation accommodationReservation)
        {
            var today = DateTime.Today;
            var leavingDay = accommodationReservation.EndDate;
            var difference = today - leavingDay;

            if (difference.Days > 5)
            {
                return true;
            }

            return false;
        }

        private static void ProcessSearchTourOption(string searchOption)
        {
            var tourRepository = new TourRepository();
            var appointmentRepository = new AppointmentRepository();
            var retVal = new List<Tour>();
            switch (searchOption)
            {
                case "1":
                    Console.WriteLine("--Search by location id--\n");
                    Console.WriteLine("Enter location id: ");

                    var id = Console.ReadLine();
                    var app = appointmentRepository.FindByLocation(Convert.ToInt32(id));
                    PrintAppointments(app);
                    break;

                case "2":
                    Console.WriteLine("--Search by tour duration--\n");
                    Console.WriteLine("Enter tour duration: ");

                    var duration = Console.ReadLine();
                    app = appointmentRepository.FindByDuration(float.Parse(duration));
                    PrintAppointments(app);
                    break;

                case "3":
                    Console.WriteLine("--Search by tour language--\n");
                    Console.WriteLine("Enter tour language: ");

                    var language = Console.ReadLine();
                    app = appointmentRepository.FindByLanguage(language);
                    PrintAppointments(app);
                    break;

                case "4":
                    Console.WriteLine("--Search by number of guests--\n");
                    Console.WriteLine("Enter number of guests: ");

                    var guestNumber = Console.ReadLine();
                    app = appointmentRepository.FindByGuestNumber(Convert.ToInt32(guestNumber));
                    PrintAppointments(app);
                    break;

                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }


        public static void PrintAppointments(List<Appointment> app)
        {
            foreach (var appointment in app)
            {
                Console.WriteLine("(ID: " + appointment.Id + ") " + appointment.Tour.Name);
                Console.WriteLine("---");
            }
        }

        private static AppointmentDTO GetAppointmentCreationData()
        {
            Console.WriteLine("Insert tour id: ");
            var tourId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert start time: ");
            var startTime = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Insert guide id: ");
            var guideId = Convert.ToInt32(Console.ReadLine());

            AppointmentDTO appointmentDTO = new AppointmentDTO(tourId, startTime, guideId);

            return appointmentDTO;
        }

        private static void ProcessCreationOption(string creationOption)
        {
            switch (creationOption)
            {
                case "1":
                    var tourDTO = new TourDTO();
                    tourDTO = GetTourCreationData();

                    var tourService = new TourService();
                    tourService.CreateTour(tourDTO);
                    break;
                case "2":
                    var appointmentDTO1 = new AppointmentDTO();
                    appointmentDTO1 = GetAppointmentCreationData();

                    AppointmentService appointmentService = new AppointmentService();
                    appointmentService.CreateAppointment(appointmentDTO1);
                    break;
                case "3":
                    KeyPointDTO keyPointDTO = new KeyPointDTO();
                    keyPointDTO = GetKeyPointCreationData();

                    KeyPointService keyPointService = new KeyPointService();
                    keyPointService.CreateKeyPoint(keyPointDTO);
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }

        public static Appointment Book(TourReservation newReservation)
        {
            var appointmentRepository = new AppointmentRepository();
            var tourReservationRepository = new TourReservationRepository();
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var createdReservation = tourReservationRepository.CreateReservation(newReservation);

            for (var i = 0; i < createdReservation.NumberOfGuests; i++)
            {
                var highestId = appointment.GuestsId.Any() ? appointment.GuestsId.Max() : 1;
                appointment.GuestsId.Add(highestId + 1);
                appointment = appointmentRepository.Update(appointment);
            }

            return appointment;
        }

        public static void ProcessCreateTourReservation(TourReservation newReservation)
        {
            var tourRepository = new TourRepository();
            var appointmentRepository = new AppointmentRepository();
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var tour = tourRepository.FindById(newReservation.TourId);
            var seatsLeft = tour.MaxGuests - appointment.GuestsId.Count;

            if (appointment.GuestsId.Count() < tour.MaxGuests)
            {
                //It's possible to make a reservation
                if (newReservation.NumberOfGuests <= seatsLeft)
                {
                    var updatedAppointment = Book(newReservation);
                    seatsLeft = tour.MaxGuests - updatedAppointment.GuestsId.Count;
                    Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
                else
                {
                    Console.WriteLine("Free seats left: {0}", seatsLeft);
                    //Update number of guests
                    Console.WriteLine("Would you like to change the number of guests? (y/n) ");
                    var answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "y":
                            Console.WriteLine("Enter new number of guests: ");
                            Console.WriteLine("Enter '0' to return");
                            var newGuestNumber = -1;
                            while (newGuestNumber == -1 || newGuestNumber > tour.MaxGuests)
                            {
                                newGuestNumber = Convert.ToInt32(Console.ReadLine());
                                if (newGuestNumber == 0)
                                    return;
                            }

                            newReservation.NumberOfGuests = newGuestNumber;
                            break;
                        case "n":
                            return;
                        default:
                            Console.WriteLine("Option does not exist");
                            break;
                    }

                    var updatedAppointment = Book(newReservation);
                    seatsLeft = tour.MaxGuests - updatedAppointment.GuestsId.Count();
                    Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
            }
            else
            {
                Console.WriteLine(
                    "Unfortunately, the tour you've chosen doesn't have any seats left.\nWould you like to pick another tour? (y/n) ");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "y":
                        Console.WriteLine("Tours on the same location: ");
                        var locationId = tour.LocationId;
                        var app = appointmentRepository.FindByLocation(Convert.ToInt32(locationId));
                        PrintAppointments(app);

                        break;
                    case "n":
                        return;
                    default:
                        Console.WriteLine("Option does not exist");
                        break;
                }
            }
        }

        private static KeyPointDTO GetKeyPointCreationData()
        {
            Console.WriteLine("Insert name: ");
            string name = Console.ReadLine();

            KeyPointDTO keyPointDTO = new KeyPointDTO(name);

            return keyPointDTO;
        }
    }
}