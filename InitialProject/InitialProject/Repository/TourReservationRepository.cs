using System;
using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";
        private readonly Serializer<TourReservation> _serializer;
        private List<TourReservation> _tourReservations;

        public TourReservationRepository() 
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(c => c.Id) + 1;
        }

        public TourReservation CreateReservation(TourReservation tourReservation) 
        {
            tourReservation.Id = NextId();
            _tourReservations = _serializer.FromCSV(FilePath);
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }

        public void ProcessSearchTourOption(string searchOption)
        {
            var tourRepository = new TourRepository();
            var appointmentRepository = new AppointmentRepository();
            var retVal = new List<Tour>();
            switch (searchOption)
            {
                case "1":
                    // --Search by location id--
                    //Enter location id:

                    var id = Console.ReadLine();
                    var app = appointmentRepository.FindByLocation(Convert.ToInt32(id));
                    Program.PrintAppointments(app);
                    break;

                case "2":
                    //Console.WriteLine("--Search by tour duration--\n");
                    //Console.WriteLine("Enter tour duration: ");

                    var duration = Console.ReadLine();
                    app = appointmentRepository.FindByDuration(float.Parse(duration));
                    Program.PrintAppointments(app);
                    break;

                case "3":
                    //Console.WriteLine("--Search by tour language--\n");
                    //Console.WriteLine("Enter tour language: ");

                    var language = Console.ReadLine();
                    app = appointmentRepository.FindByLanguage(language);
                    Program.PrintAppointments(app);
                    break;

                case "4":
                    //Console.WriteLine("--Search by number of tourists--\n");
                    //Console.WriteLine("Enter number of tourists: ");

                    var guestNumber = Console.ReadLine();
                    app = appointmentRepository.FindByGuestNumber(Convert.ToInt32(guestNumber));
                    Program.PrintAppointments(app);
                    break;

                case "x":
                    break;
                default:
                    //Console.WriteLine("Option does not exist");
                    break;
            }
        }
    }
}
