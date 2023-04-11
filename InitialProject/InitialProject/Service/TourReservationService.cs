using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;
using System.Linq;

namespace InitialProject.Service
{
    public class TourReservationService
    {
        TourReservationRepository tourReservationRepository = new TourReservationRepository();

        public void Book(TourReservation newReservation)
        {
            var appointmentRepository = new TourAppointmentRepository();
            var tourReservationRepository = new TourReservationRepository();
            var tourRepository = new TourRepository();

            var appointment = appointmentRepository.FindById(newReservation.TourId);
            var tour = tourRepository.FindById(newReservation.TourId);
            var createdReservation = tourReservationRepository.CreateReservation(newReservation);
            appointment.TouristIds.Add(newReservation.TouristId);
            appointment.AvailableSeats -= newReservation.NumberOfTourists;
            appointment = appointmentRepository.Update(appointment);

        }

        /*public void ProcessCreateTourReservation(TourReservation newReservation)
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
                    var touristId = 1;
                    var updatedAppointment = Book(newReservation, touristId);
                    seatsLeft = tour.MaxTourists - updatedAppointment.TouristsId.Count;
                    //Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
                else
                {
                    //Console.WriteLine("Free seats left: {0}", seatsLeft);
                    //Update number of tourists
                    //Console.WriteLine("Would you like to change the number of tourists? (y/n) ");
                    //var answer = Console.ReadLine();
                   // switch (answer)
                    //{
                      //  case "y":
                            //Console.WriteLine("Enter new number of tourists: ");
                            //Console.WriteLine("Enter '0' to return");
                            var newTouristNumber = -1;
                            while (newTouristNumber == -1 || newTouristNumber > tour.MaxTourists)
                            {
                                newTouristNumber = Convert.ToInt32(Console.ReadLine());
                                if (newTouristNumber == 0)
                                    return;
                            }

                            newReservation.NumberOfTourists = newTouristNumber;
                         //  break;
                        case "n":
                            return;
                        default:
                            //Console.WriteLine("Option does not exist");
                            break;
                    }

                    //var updatedAppointment = Book(newReservation);
                    //seatsLeft = tour.MaxTourists - updatedAppointment.TouristsId.Count();
                    //Console.WriteLine("Successfully booked tour!\nFree seats left: {0}", seatsLeft);
                }
            }
            else
            {
                //Console.WriteLine(
                    //"Unfortunately, the tour you've chosen doesn't have any seats left.\nWould you like to pick another tour? (y/n) ");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "y":
                        //Console.WriteLine("Tours on the same location: ");
                        var locationId = tour.LocationId;
                        var app = appointmentRepository.FindByLocation(Convert.ToInt32(locationId));
                        //Program.PrintAppointments(app);

                        break;
                    case "n":
                        return;
                    default:
                        //Console.WriteLine("Option does not exist");
                        break;
                }*/
            //}
        //}
    }
}
