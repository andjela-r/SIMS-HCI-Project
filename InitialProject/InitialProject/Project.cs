using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine("Option 5\n");
                    /*TourService tourService = new TourService();

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

                    
                    Console.WriteLine("How many key points do you want to enter?  ");
                    int nkp = Convert.ToInt32(Console.ReadLine());
                    for(int i = 0; i<nkp; i++)
                    {
                        Console.WriteLine("Insert key point id: ");
                        int keyPointId = Convert.ToInt32(Console.ReadLine());
                    }
                    */
                    break;
                case "6":
                    Console.WriteLine("Option 6\n");
                    break;
                case "7":
                    TourRepository tourRepository = new TourRepository();
                    Console.WriteLine("Unesite jezik: ");
                    string language = Console.ReadLine();
                    List<Tour> retVal = tourRepository.FindByLanguage(language);
                    foreach (Tour tour in retVal)
                    {
                        Console.WriteLine(tour.Name);
                        Console.WriteLine("---");
                    }
                    break;
                case "8":
                    Console.WriteLine("Option 8\n");
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }
    }
}

