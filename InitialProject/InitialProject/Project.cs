using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("1. Accommodation registration");
            Console.WriteLine("2. Rate a guest");
            Console.WriteLine("\n3. Search Accommodation");
            Console.WriteLine("4. Accommodation reservation");
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
            Console.WriteLine("4. Search by number of tourists");
            Console.WriteLine("\nx. Exit");
            Console.Write("Your option: ");
        }

        private static void WriteMenuOptions1()
        {
            Console.WriteLine("1. By name");
            Console.WriteLine("2. By location");
            Console.WriteLine("3. By type");
            Console.WriteLine("4. By minimum days");
            Console.WriteLine("5. By occupancy");
            Console.WriteLine("x. Exit");
            Console.Write("Your option: \n");
        }

        private static void ProcessChosenOption1(string searchOption)
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            switch (searchOption)
            {
                case "1":
                    Console.WriteLine("Option 1\n");
                    Console.WriteLine("Enter name of the accommodation: ");
                    string naziv = Console.ReadLine();
                    List<Accommodation> listAcc4 = accommodationRepository.FindByName(naziv);
                    foreach (Accommodation accommodation in listAcc4)
                    {
                        Console.WriteLine(accommodation.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "2":
                    Console.WriteLine("Option 2\n");
                    Console.WriteLine("Enter location of the accommodation: ");
                    int lokacija = Convert.ToInt32(Console.ReadLine());
                    List<Accommodation> listAcc3 = accommodationRepository.FindByLocation(lokacija);
                    foreach (Accommodation accommodation in listAcc3)
                    {
                        Console.WriteLine(accommodation.Name);
                        Console.WriteLine("Location: " + accommodation.LocationId);
                        Console.WriteLine("---");
                    }
                    break;
                case "3":
                    Console.WriteLine("Option 3\n");
                    Console.WriteLine("What type of the accommodation do you want: ");
                    Model.Type tip = (Model.Type)Convert.ToInt32(Console.ReadLine());
                    List<Accommodation> listAcc2 = accommodationRepository.FindByType(tip);
                    foreach (Accommodation accommodation in listAcc2)
                    {
                        Console.WriteLine(accommodation.Name);
                        Console.WriteLine("Type: " + accommodation.Type);
                        Console.WriteLine("---");
                    }
                    break;
                case "4":
                    Console.WriteLine("Option 4\n");
                    Console.WriteLine("How many days do you want to stay in: ");
                    int minimum = Convert.ToInt32(Console.ReadLine());
                    List<Accommodation> listAcc1 = accommodationRepository.FindByMinStay(minimum);
                    foreach (Accommodation accommodation in listAcc1)
                    {
                        Console.WriteLine(accommodation.Name);
                        Console.WriteLine("Min days: " + accommodation.MinStay);
                        Console.WriteLine("---");
                    }
                    break;
                case "5":
                    Console.WriteLine("Option 5\n");
                    Console.WriteLine("How many guests do you want to stay in: ");
                    int maks = Convert.ToInt32(Console.ReadLine());
                    List<Accommodation> listAcc = accommodationRepository.FindByMaxGuests(maks);
                    foreach (Accommodation accommodation in listAcc)
                    {
                        Console.WriteLine(accommodation.Name);
                        Console.WriteLine("Max guests: " + accommodation.MaxGuests);
                        Console.WriteLine("---");
                    }
                    break;
                case "x":
                    break;
            }


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
                    /*do
                    {
                        RateAGuest();
                    } while (!chosenOption.Equals("x"));

                    break;*/
                case "3":
                    string searchOption;
                    do
                    {
                        WriteMenuOptions1();
                        searchOption = Console.ReadLine();
                        Console.Clear();
                        ProcessChosenOption1(searchOption);
                    } while (!searchOption.Equals("x"));

                    break;
                case "4":
                   /* AccommodationRepository accommodationRepository = new AccommodationRepository();
                    Console.WriteLine("Option 4\n");
                    Console.WriteLine("Enter id of the accommodation: ");
                    int accId = Convert.ToInt32(Console.ReadLine());
                    while (accommodationRepository.FindById(accId) == null)
                    {
                        Console.WriteLine("Accommodation does not exists: ");
                        Console.WriteLine("Enter id of the accommodation: ");
                        accId = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("Enter id of the guest: ");
                    int guestId1 = Convert.ToInt32(Console.ReadLine());
                    DateTime startDate;
                    DateTime endDate;
                    var accommodation = accommodationRepository.FindById(accId);

                    do
                    {
                        Console.WriteLine("Enter start date: (dd/MM/yyyy)");
                        string line = Console.ReadLine();
                        while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
                        {
                            Console.WriteLine("Invalid");
                            line = Console.ReadLine();
                        }

                        Console.WriteLine("Enter end date: (dd/MM/yyyy) ");
                        string line1 = Console.ReadLine();
                        while (!DateTime.TryParseExact(line1, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
                        {
                            Console.WriteLine("Invalid");
                            line1 = Console.ReadLine();
                        }
                    }
                    while (startDate >= endDate || startDate < DateTime.Today || (endDate - startDate).TotalDays < accommodation.MinStay);

                    Console.WriteLine("How many days do you want to stay in: ");
                    int d = Convert.ToInt32(Console.ReadLine());
                    while((endDate - startDate).TotalDays < d)
                    {
                        Console.WriteLine("number of days must be smaller than period. ");
                        d = Convert.ToInt32(Console.ReadLine());
                    }
                    while(accommodation.MinStay >= d)
                    {
                        Console.WriteLine("Minimum number of days to stay in is " + accommodation.MinStay);
                        d = Convert.ToInt32(Console.ReadLine());
                    }
                    ProcessCreateAccommodationReservation(accId, guestId1, startDate, endDate, d);
                    break;*/
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
                    do
                    {
                        WriteTourSearchOptions();
                        searchOption = Console.ReadLine();
                        Console.Clear();
                        //TourReservationService.TourReservationRepository.ProcessSearchTourOption(searchOption);
                    } while (!searchOption.Equals("x"));

                    break;
                case "8":
                    Console.WriteLine("Enter tour id: ");
                    var tourId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter guest id: ");
                    var guestId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter number of tourists: ");
                    var guestNumber = Convert.ToInt32(Console.ReadLine());

                    var newReservation = new TourReservation(guestNumber, guestId, tourId);
                    //TourReservationService.ProcessCreateTourReservation(newReservation);

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
            Console.WriteLine("4. Find upcoming appointments");
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

            Console.WriteLine("Insert max number of tourists: ");
            var maxTourists = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert duration: ");
            var duration = float.Parse(Console.ReadLine());

            Console.WriteLine("Insert picture links: ");
            var pictures = Console.ReadLine().Split(',');
            var pictureLinks = pictures.ToList();
            
            var tourDTO = new TourDTO(name, locationId, description, language, maxTourists, duration,
                pictureLinks);

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
            List<string> pictureLinks = pictures.ToList();

            AccommodationDTO smestaj = new AccommodationDTO(name,
                idLocation, (Type)type, maxOccupancy, minDays, cancelPeriod, pictureLinks);
            accommodationService.CreateAccommodation(smestaj);
        }

        /*private static void RateAGuest()
        {
             //this will be activated with front
             AccommodationReservation accommodationReservation = new AccommodationReservation();
             if (!isDeadlineOver(accommodationReservation))
             {
                 Console.WriteLine("You have not rated a guest yet.");
             } else Console.WriteLine("You have already rated a guest.");

            int cleanliness, obedience;
            GuestRatingService guestRatigService = new GuestRatingService();

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
        }*/

        /*private static bool IsDeadlineOver(AccommodationReservation accommodationReservation)
        {
            var today = DateTime.Today;
            var leavingDay = accommodationReservation.EndDate;
            var difference = today - leavingDay;

            if (difference.Days > 5)
            {
                return true;
            }

            return false;
        }*/


        /*public static void PrintAppointments(List<Appointment> app)
        {
            foreach (var appointment in app)
            {
                Console.WriteLine("(ID: " + appointment.Id + ") " + appointment.Tour.Name);
                Console.WriteLine("---");
            }
        }*/

        private static TourAppointmentDTO GetAppointmentCreationData()
        {
            Console.WriteLine("Insert tour id: ");
            var tourId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert start time: ");
            var startTime = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Insert key points: ");
            var keyPoints = Console.ReadLine().Split(',');
            var keyPointIds = keyPoints.Select(int.Parse).ToList();

            TourAppointmentDTO appointmentDTO = new TourAppointmentDTO(tourId, startTime, keyPointIds);

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
                    var appointmentDTO1 = new TourAppointmentDTO();
                    appointmentDTO1 = GetAppointmentCreationData();
                    TourAppointmentService appointmentService = new TourAppointmentService();
                    appointmentService.CreateAppointment(appointmentDTO1);
                    break;
                case "3":
                    KeyPointDTO keyPointDTO = new KeyPointDTO();
                    keyPointDTO = GetKeyPointCreationData();

                    KeyPointService keyPointService = new KeyPointService();
                    keyPointService.CreateKeyPoint(keyPointDTO);
                    break;
                case "4":
                    TourAppointmentService tourAppointmentService = new TourAppointmentService();
                    tourAppointmentService.CancelUpcomingAppointment();
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }

        /*public static void ProcessCreateTourReservation(TourReservation newReservation)
        {
            var tourRepository = new TourRepository();
            var appointmentRepository = new TourAppointmentRepository();
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var tour = tourRepository.FindById(newReservation.TourId);
            var seatsLeft = tour.MaxTourists - appointment.TouristIds.Count;

            if (appointment.TouristIds.Count() < tour.MaxTourists)
            {
                //It's possible to make a reservation
                if (newReservation.NumberOfTourists <= seatsLeft)
                {
                    var updatedAppointment = TourReservationService.Book(newReservation);
                    seatsLeft = tour.MaxTourists - updatedAppointment.TouristsId.Count;
                    Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
                else
                {
                    Console.WriteLine("Free seats left: {0}", seatsLeft);
                    //Update number of tourists
                    Console.WriteLine("Would you like to change the number of tourists? (y/n) ");
                    var answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "y":
                            Console.WriteLine("Enter new number of tourists: ");
                            Console.WriteLine("Enter '0' to return");
                            var newTouristNumber = -1;
                            while (newTouristNumber == -1 || newTouristNumber > tour.MaxTourists)
                            {
                                newTouristNumber = Convert.ToInt32(Console.ReadLine());
                                if (newTouristNumber == 0)
                                    return;
                            }

                            newReservation.NumberOfTourists = newTouristNumber;
                            break;
                        case "n":
                            return;
                        default:
                            Console.WriteLine("Option does not exist");
                            break;
                    }

                    var updatedAppointment = TourReservationService.Book(newReservation);
                    seatsLeft = tour.MaxTourists - updatedAppointment.TouristIds.Count();
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
        }*/

        private static KeyPointDTO GetKeyPointCreationData()
        {
            Console.WriteLine("Insert name: ");
            string name = Console.ReadLine();

            KeyPointDTO keyPointDTO = new KeyPointDTO(name);

            return keyPointDTO;
        }

        /*public static void ProcessCreateAccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate, int duration)
        {
            List<Accommodation> retVal = new List<Accommodation>();
            List<AccommodationReservation> retVal1 = new List<AccommodationReservation>();
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();

            var accommodation = accommodationRepository.FindById(accommodationId);
            //var reservation = accommodationReservationRepository.FindByAccommodationId(accommodationId);
            //List<AccommodationReservation> listAcc5 = accommodationReservationRepository.FindByAccommodationId(accommodation.Id);
            List<AccommodationReservation> listAcc = accommodationReservationRepository.FindByAccommodationId(accommodationId);
            foreach (AccommodationReservation accommodation1 in listAcc)
            {
                Console.WriteLine(accommodation1.AccommodationId);
                Console.WriteLine(accommodationId);

                if (accommodation1.AccommodationId+1 == accommodationId)
                {
                    Console.WriteLine("***");
                    if (accommodation1.StartDate < startDate)
                    {
                        if (accommodation1.EndDate < endDate)
                        {
                            var diffEnd = endDate - accommodation1.EndDate;
                            Console.WriteLine("free days: " + diffEnd.Days + "\n");
                            Console.WriteLine("You want this number of days to reserve : " + duration);
                            if (diffEnd.Days >= duration)
                            {
                                Console.WriteLine("Available dates:");
                                List<DateTime> listAcc5 =
                                    accommodationReservationRepository.GetOccupiedDays(accommodation1.EndDate, endDate);
                                Console.WriteLine("Slobodni dani:  ");
                                int i = 1;
                                foreach (DateTime date in listAcc5)
                                {
                                    if (date.AddDays(duration) <= endDate)
                                        Console.WriteLine("Date " + i + ": " + date.Day + "." + date.Month);
                                    i++;
                                }

                                Console.WriteLine("Pick a date: ");
                                int n = Convert.ToInt32(Console.ReadLine());
                                DateTime newStartDate = listAcc5[n - 1];
                                DateTime newEndDate = newStartDate.AddDays(duration);
                                Console.WriteLine("How many guests will come: ");
                                int guests = Convert.ToInt32(Console.ReadLine());
                                Accommodation acc = accommodationRepository.FindById(accommodationId);

                                while (acc.MaxGuests > guests)
                                {
                                    Console.WriteLine("Too many guests: ");
                                    guests = Convert.ToInt32(Console.ReadLine());
                                }

                                AccommodationReservation newReservation1 = new AccommodationReservation(accommodationId,
                                    guestId, newStartDate, newEndDate, duration);
                                Console.WriteLine("Succesful! ");
                            }
                            else
                                Console.WriteLine("cant reserve this accommodation for this period. Sorry!");

                            break;
                        }
                        else
                        {
                            Console.WriteLine("cant reserve this accommodation for this period. Sorry!");
                            break;
                        }
                    }
                    else
                    {
                        if (accommodation1.EndDate > endDate)
                        {
                            var diffStart = accommodation1.StartDate - startDate;
                            Console.WriteLine("free days: " + diffStart.Days + "\n");
                            Console.WriteLine("You want this number of days to reserve : " + duration);
                            if (diffStart.Days > duration)
                            {
                                Console.WriteLine("Available dates: ");
                                List<DateTime> listAcc6 =
                                    accommodationReservationRepository.GetOccupiedDays(startDate,
                                        accommodation1.StartDate);
                                Console.WriteLine("Slobodni dani:  ");
                                int i = 1;
                                foreach (DateTime date in listAcc6)
                                {
                                    if (date.AddDays(duration) <= startDate)
                                        Console.WriteLine("Date " + i + ": " + date.Day + "." + date.Month);
                                    i++;
                                }

                                Console.WriteLine("Pick a date: ");
                                int n = Convert.ToInt32(Console.ReadLine());
                                DateTime newStartDate = listAcc6[n - 1];
                                DateTime newEndDate = newStartDate.AddDays(duration);

                                Console.WriteLine("How many guests will come: ");
                                int guests = Convert.ToInt32(Console.ReadLine());
                                Accommodation acc = accommodationRepository.FindById(accommodationId);

                                while (acc.MaxGuests > guests)
                                {
                                    Console.WriteLine("Too many guests: ");
                                    guests = Convert.ToInt32(Console.ReadLine());
                                }

                                AccommodationReservation newReservation1 = new AccommodationReservation(accommodationId,
                                    guestId, newStartDate, newEndDate, duration);

                                Console.WriteLine("Successful! ");
                            }
                            else
                                Console.WriteLine("cant reserve this accommodation for this period. Sorry!");

                            break;
                        }
                        else
                        {
                            var diffStart = startDate - accommodation1.StartDate;
                            var diffEnd = endDate - accommodation1.EndDate;
                            Console.WriteLine("You want this number of days to reserve : " + duration);

                            if (diffStart.Days > duration || diffEnd.Days > duration)
                            {
                                Console.WriteLine("Available dates:");
                                List<DateTime> listAcc5 =
                                    accommodationReservationRepository.GetOccupiedDays(startDate,
                                        accommodation1.StartDate);
                                int i = 1;
                                foreach (DateTime date in listAcc5)
                                {
                                    if (date.AddDays(duration) <= accommodation1.StartDate)
                                        Console.WriteLine("Date " + i + ": " + date.Day + "." + date.Month);
                                    i++;
                                }

                                Console.WriteLine("OR: \n\n");
                                List<DateTime> listAcc6 =
                                    accommodationReservationRepository.GetOccupiedDays(accommodation1.EndDate, endDate);
                                foreach (DateTime date in listAcc6)
                                {
                                    if (date.AddDays(duration) <= endDate)
                                        Console.WriteLine("Date " + i + ": " + date.Day + "." + date.Month);
                                    i++;
                                }

                                Console.WriteLine("Pick a date: ");
                                int n = Convert.ToInt32(Console.ReadLine());

                                DateTime newStartDate = listAcc5[n - 1];
                                DateTime newEndDate = newStartDate.AddDays(duration);

                                Console.WriteLine("How many guests will come: ");
                                int guests = Convert.ToInt32(Console.ReadLine());
                                Accommodation acc = accommodationRepository.FindById(accommodationId);

                                while (acc.MaxGuests > guests)
                                {
                                    Console.WriteLine("Too many guests: ");
                                    guests = Convert.ToInt32(Console.ReadLine());
                                }

                                AccommodationReservation newReservation1 = new AccommodationReservation(accommodationId,
                                    guestId, newStartDate, newEndDate, duration);
                                var newReservation3 =
                                    accommodationReservationRepository.CreateReservation(newReservation1);
                                Console.WriteLine("Succesful! ");
                            }
                            else
                                Console.WriteLine("cant reserve this accommodation for this period. Sorry!");

                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(accommodation1.AccommodationId);
                    Console.WriteLine(accommodationId);
                    Console.WriteLine("///Unesi datume kada zelite: ");
                    DateTime starttDate;
                    DateTime enddDate;
                    // var accommodation = accommodationRepository.FindById(accId);

                    do
                    {
                        Console.WriteLine("Enter start date: (dd/MM/yyyy)");
                        string line = Console.ReadLine();
                        while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null,
                                   System.Globalization.DateTimeStyles.None, out starttDate))
                        {
                            Console.WriteLine("Invalid");
                            line = Console.ReadLine();
                        }

                        Console.WriteLine("Enter end date: (dd/MM/yyyy) ");
                        string line1 = Console.ReadLine();
                        while (!DateTime.TryParseExact(line1, "dd/MM/yyyy", null,
                                   System.Globalization.DateTimeStyles.None, out enddDate))
                        {
                            Console.WriteLine("Invalid");
                            line1 = Console.ReadLine();
                        }
                    } while (starttDate >= enddDate || starttDate < DateTime.Today);

                    AccommodationReservation newReservation9 =
                        new AccommodationReservation(accommodationId, guestId, starttDate, enddDate, duration);
                    var newReservation4 = accommodationReservationRepository.CreateReservation(newReservation9);

                     Console.WriteLine("Available dates: ");
                     List<DateTime> listAcc5 = accommodationReservationRepository.GetOccupiedDays(startDate, endDate);
                     Console.WriteLine("Slobodni dani:  ");
                     int i = 1;
                     foreach (DateTime date in listAcc5)
                     {
                         if (date.AddDays(duration) <= startDate)
                             Console.WriteLine("Date " + i + ": " + date.Day + "." + date.Month);
                         i++;
                     }
                     Console.WriteLine("Pick a date: ");
                     int n = Convert.ToInt32(Console.ReadLine());
                     DateTime newStartDate = listAcc5[n - 1];
                     DateTime newEndDate = newStartDate.AddDays(duration);

                     Console.WriteLine("How many guests will come: ");
                     int guests = Convert.ToInt32(Console.ReadLine());
                     Accommodation acc = accommodationRepository.FindById(accommodationId);

                     while (acc.MaxGuests > guests)
                     {
                         Console.WriteLine("Too many guests: ");
                         guests = Convert.ToInt32(Console.ReadLine());
                     }
                     AccommodationReservation newReservation1 = new AccommodationReservation(accommodationId, guestId, newStartDate, newEndDate, duration);
                     Console.WriteLine("Succesful! ");
                }
            }
            }*/
        }
    }

