using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

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
                    Console.WriteLine("Option 1\n");
                    break;
                case "2":
                    Console.WriteLine("Option 2\n");
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
                    //ispis danasnjih tura
                    TourRepository tourRepository = new TourRepository();
                    AppointmentRepository appointmentRepository = new AppointmentRepository();
                    List<Appointment> appointments = appointmentRepository.FindTodaysAppointments();
                    foreach(Appointment appointment in appointments)
                    {
                        Tour tour = tourRepository.FindById(appointment.TourId);
                        Console.WriteLine(appointment.TourId + " "+ tour.Name);
                    }

                    //zapocinjanje ture - promena statusa
                    Console.WriteLine("Select a tour(id)");
                    int SelectTourId = Convert.ToInt32(Console.ReadLine());
                    appointmentRepository.StartTodaysAppointment(SelectTourId);
                    Tour selectedTour = tourRepository.FindById(SelectTourId);

                    //ispis kljucnih tacaka
                    //KeyPointRepository keyPointRepository = new KeyPointRepository();
                    //List<KeyPoint> keyPoints = keyPointRepository.FindKeyPoints(selectedTour);
                    //foreach (KeyPoint keyPoint in keyPoints)
                    //{
                        //Console.WriteLine(keyPoint.Name);
                    //}

                    //menjanje statusa kljucnih tacaka
                    

                    //provera gostiju

                    //kraj ture - zavrsena poslednja kljucna tacka || nepredvidive okolnosti

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
                    int tourId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter guest id: ");
                    int guestId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter number of guests: ");
                    int guestNumber = Convert.ToInt32(Console.ReadLine());

                    TourReservation newReservation = new TourReservation(guestNumber, guestId, tourId);
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
            Console.WriteLine("x. Exit");

            Console.WriteLine("Your option: ");
        }

        private static TourDTO GetTourCreationData()
        {
            Console.WriteLine("Insert name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Insert location id: ");
            int locationId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Insert language: ");
            string language = Console.ReadLine();

            Console.WriteLine("Insert max number of guests: ");
            int maxGuests = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert key points: ");
            var keyPoints = Console.ReadLine().Split(',');
            List<int> keyPointsId = keyPoints.Select(Int32.Parse).ToList();

            Console.WriteLine("Insert duration: ");
            float duration = float.Parse(Console.ReadLine());

            Console.WriteLine("Insert picture ids: ");
            var pictures = Console.ReadLine().Split(',');
            List<int> pictureIds = pictures.Select(Int32.Parse).ToList();

            TourDTO tourDTO = new TourDTO(name, locationId, description, language, maxGuests, keyPointsId, duration, pictureIds);

            return tourDTO;            
        }


        private static void ProcessSearchTourOption(string searchOption)
        {
            TourRepository tourRepository = new TourRepository();
            List<Tour> retVal = new List<Tour>();
            switch (searchOption)
            {
                case "1":
                    Console.WriteLine("--Search by location id--\n");
                    Console.WriteLine("Enter location id: ");
                    string id = Console.ReadLine();
                    retVal = tourRepository.FindByLocation(Convert.ToInt32(id));
                    foreach (Tour tour in retVal)
                    {
                        Console.WriteLine(tour.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "2":
                    Console.WriteLine("--Search by tour duration--\n");
                    Console.WriteLine("Enter tour duration: ");
                    string duration = Console.ReadLine();
                    retVal = tourRepository.FindByDuration(Convert.ToInt32(duration));
                    foreach (Tour tour in retVal)
                    {
                        Console.WriteLine(tour.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "3":
                    Console.WriteLine("--Search by tour language--\n");
                    Console.WriteLine("Enter tour language: ");
                    string language = Console.ReadLine();
                    retVal = tourRepository.FindByLanguage(language);
                    foreach (Tour tour in retVal)
                    {
                        Console.WriteLine(tour.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "4":
                    Console.WriteLine("--Search by number of guests--\n");
                    Console.WriteLine("Enter number of guests: ");
                    string guestNumber = Console.ReadLine();
                    retVal = tourRepository.FindByGuestNumber(Convert.ToInt32(guestNumber));
                    foreach (Tour tour in retVal)
                    {
                        Console.WriteLine(tour.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }

        private static AppointmentDTO GetAppointmentCreationData()
        {
            Console.WriteLine("Insert tour id: ");
            int tourId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert start time: ");
            DateTime startTime = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Insert guide id: ");
            int guideId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert guest ids: ");
            var guests = Console.ReadLine().Split(',');
            List<int> guestIds = guests.Select(Int32.Parse).ToList();

            AppointmentDTO appointmentDTO = new AppointmentDTO(tourId, startTime, guideId, guestIds);

            return appointmentDTO;
        }

        private static void ProcessCreationOption(string creationOption)
        {
            switch (creationOption)
            {
                case "1":
                    TourDTO tourDTO = new TourDTO();
                    tourDTO = GetTourCreationData();

                    TourService tourService = new TourService();
                    tourService.CreateTour(tourDTO);
                    break;
                case "2":
                    AppointmentDTO appointmentDTO1 = new AppointmentDTO();
                    appointmentDTO1 = GetAppointmentCreationData();

                    AppointmentService appointmentService = new AppointmentService();
                    appointmentService.CreateAppointment(appointmentDTO1);
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
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            TourReservationRepository tourReservationRepository = new TourReservationRepository();
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var createdReservation = tourReservationRepository.CreateReservation(newReservation);

            for (int i = 0; i < createdReservation.NumberOfGuests; i++)
            {
                var highestId = appointment.GuestsId.Any() ? appointment.GuestsId.Max() : 1;
                appointment.GuestsId.Add(highestId + 1);
                appointment = appointmentRepository.Update(appointment);
            }
            return appointment;
        }

        public static void ProcessCreateTourReservation(TourReservation newReservation)
        {
            List<Tour> retVal = new List<Tour>();
            TourRepository tourRepository = new TourRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            //ne vraca apdejtovan appointment u drugom krugu
            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var tour = tourRepository.FindById(newReservation.TourId);
            int seatsLeft = tour.MaxGuests - appointment.GuestsId.Count();

            if (appointment.GuestsId.Count() < tour.MaxGuests)
            {
                //Moguce je napraviti rezervaciju
                if (newReservation.NumberOfGuests <= seatsLeft)
                {
                    var updatedAppointment = Book(newReservation);
                    seatsLeft = tour.MaxGuests - updatedAppointment.GuestsId.Count();
                    Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);

                }
                else //newReservation.NumberOfGuests > appointment.GuestsId.Count()
                {
                    Console.WriteLine("Free seats left: {0}", seatsLeft);
                    //Update number of guests
                    Console.WriteLine("Would you like to change the number of guests? (y/n) ");
                    string answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "y":
                            Console.WriteLine("Enter new number of guests: ");
                            Console.WriteLine("Enter '0' to return");
                            int newGuestNumber = -1;
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
                Console.WriteLine("Unfortunately, the tour you've chosen doens't have any seats left.\nWould you like to pick another tour? (y/n) ");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "y":
                        Console.WriteLine("Tours on the same location: ");
                        int locationId = tour.LocationId;
                        retVal = tourRepository.FindByLocation(locationId);
                        foreach (Tour tours in retVal)
                        {
                            Console.WriteLine(tour.Name);
                            Console.WriteLine("---");
                        }
                        
                        break;
                    case "n":
                        return;
                    default:
                        Console.WriteLine("Option does not exist");
                        break;
                }
            }
        }
    }
}
