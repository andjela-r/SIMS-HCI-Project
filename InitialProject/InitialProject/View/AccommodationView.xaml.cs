using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Type = InitialProject.Model.Type;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationView.xaml
    /// </summary>
    public partial class AccommodationView : Window
    {
        private readonly AccommodationRepository _accomodationRepository;
        private readonly LocationRepository _locationRepository;
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public static List<Location> Locations { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public User Guest { get; set; }

        public AccommodationView(User guest)
        {
            InitializeComponent();
            DataContext = this;
            this.Guest = guest;
            Guest = guest;
            _accomodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_accomodationRepository.FindAll());
            Locations = new List<Location>(_locationRepository.FindAll());
            foreach (Accommodation accommodation in Accommodations)
            {
                accommodation.Location = Locations.Find(l => l.Id == accommodation.LocationId);
            }
            foreach (Accommodation accommodation in Accommodations)
            {
                if (accommodation.Type == Type.House)
                    accommodation.TypeText = "House";
                else if (accommodation.Type == Type.Apartment)
                    accommodation.TypeText = "Apartment";
                else
                    accommodation.TypeText = "Cottage";
            }

            foreach (Accommodation accommodation in Accommodations)
            {
                LocationComboBox.Items.Add(accommodation.Location.City);
            }
            LocationComboBox.Items.Insert(0, "");

            FilteredAccommodations = new ObservableCollection<Accommodation>();
            Types = new ObservableCollection<string>();
            TypeBox.Items.Add("");
            TypeBox.Items.Add("Apartment");
            TypeBox.Items.Add("House");
            TypeBox.Items.Add("Cottage");
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilteredAccommodations.Clear();
            foreach (Accommodation accommodation in Accommodations)
            {
                if (IsAccommodationMatchingSearchCriteria(accommodation))
                {
                    if(!FilteredAccommodations.Contains(accommodation))
                        FilteredAccommodations.Add(accommodation);
                    dataGridAccommodations.ItemsSource = FilteredAccommodations;
                }
            }

            dataGridAccommodations.ItemsSource = FilteredAccommodations;
        }

        public bool IsAccommodationMatchingSearchCriteria(Accommodation accommodation)
        {
            string name = NameBox.Text.ToLower();
            string[] nameWords = name.Split(' ');
            string location = LocationComboBox.Text.Replace(",", "").Replace(" ", "");
            string type = (string)TypeBox.SelectedItem;
            string guestNumber = GuestNumBox.Text;
            string daysForReservation = MinDaysBox.Text;
            bool matches = false;

            if ((IsContainingNameWords(accommodation, nameWords) || string.IsNullOrEmpty(name)) &&
                (accommodation.Location.City.Replace(" ", "").Contains(location) || string.IsNullOrEmpty(location))&&
                (HasMatchingAccommodationType(accommodation, type) || string.IsNullOrEmpty(type)) &&
                (IsGuestNumberLessThanMaximum(accommodation, guestNumber) || string.IsNullOrEmpty(guestNumber)) &&
                (IsReservationGreaterThanMinimum(accommodation, daysForReservation) ||
                 string.IsNullOrEmpty(daysForReservation)))
                matches = true;
            return matches;
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

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeBox.SelectedItem != null)
            {
                string inputType = (string)TypeBox.SelectedItem;
            }
        }

        private void dataGridAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccommodationReservationView reservationView = new AccommodationReservationView(SelectedAccommodation, Guest);
                reservationView.Show();
            }
            else
            {
                MessageBox.Show("Please select accommodation for reservation.");
            }
        }

    }
}
